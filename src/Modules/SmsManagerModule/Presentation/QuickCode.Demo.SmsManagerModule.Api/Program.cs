using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.SmsManagerModule.Persistence.Contexts;
using System.Text.Json.Serialization;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Filters;
using QuickCode.Demo.Common.Helpers;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Nswag.Extensions;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.Common.Mappers;
using Serilog;

using AspNetCoreRateLimit;
using Polly;
using Polly.Extensions.Http;
using Polly.CircuitBreaker;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using OpenTelemetry.Metrics;
using OpenTelemetry.Logs;
using QuickCode.Demo.Common.Middleware;

var builder = WebApplication.CreateBuilder(args);
var runMigration = Environment.GetEnvironmentVariable("RUN_MIGRATION");

builder.Configuration.UpdateConfigurationFromEnv();

var useHealthCheck = builder.Configuration.GetSection("AppSettings:UseHealthCheck").Get<bool>();
var databaseType = builder.Configuration.GetSection("AppSettings:DatabaseType").Get<string>();
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

builder.Services.AddLogger(builder.Configuration);
Log.Information($"Started({environmentName}) {builder.Configuration["Logging:ApiName"]} Started.");

builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimit"));
builder.Services.Configure<ClientRateLimitOptions>(builder.Configuration.GetSection("ClientRateLimit"));
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddHttpClient("api")
    .AddPolicyHandler(GetCircuitBreakerPolicy())
    .AddPolicyHandler(GetRetryPolicy());

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService(serviceName: "QuickCode.Demo.SmsManagerModule.Api"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddJaegerExporter(options =>
        {
            options.AgentHost = builder.Configuration["Jaeger:Host"] ?? "localhost";
            options.AgentPort = int.Parse(builder.Configuration["Jaeger:Port"] ?? "6831");
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddPrometheusExporter());

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});



builder.Services.AddQuickCodeMediator<Program>();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer(typeof(Program))));
    options.Filters.Add(typeof(ApiLogFilterAttribute));
    options.Filters.Add(new ProducesAttribute("application/json"));
}).AddJsonOptions(jsonOptions => { jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();
builder.Services.AddQuickCodeDbContext<ReadDbContext, WriteDbContext>(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddQuickCodeRepositories(typeof(WriteDbContext));
builder.Services.AddQuickCodeSwaggerGen(builder.Configuration);
builder.Services.AddNswagServiceClient(builder.Configuration, typeof(Program));
builder.Services.AddServiceClient(builder.Configuration, typeof(Program));

DapperTypeMapper.ConfigureTypeMappings();
var app = builder.Build();

static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(
            handledEventsAllowedBeforeBreaking: 3,
            durationOfBreak: TimeSpan.FromSeconds(30),
            onBreak: (result, duration) =>
            {
                Log.Warning("Circuit breaker opened for {Duration} seconds", duration.TotalSeconds);
            },
            onReset: () =>
            {
                Log.Information("Circuit breaker reset");
            },
            onHalfOpen: () =>
            {
                Log.Information("Circuit breaker half-open");
            });
}

static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.BadRequest)
        .WaitAndRetryAsync(3, retry => TimeSpan.FromSeconds(Math.Pow(2, retry)),
            onRetry: (result, timeSpan, retryCount, context) =>
            {
                Log.Warning("Retry {RetryCount} after {Delay}ms", retryCount, timeSpan.TotalMilliseconds);
            });
}

app.UseSerilogRequestLogging();
app.UseResponseCompression();

app.UseIpRateLimiting();
app.UseClientRateLimiting();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (useHealthCheck && databaseType != "inMemory")
{
    app.UseHealthChecks("/hc", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
}

app.UseExceptionHandler("/error");

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseSecurityHeaders();
app.UseRateLimiting();
app.UseInputValidation();
app.UseSecurityLogging();
app.UseSecurityAudit();
app.UsePasswordPolicy();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    if ((runMigration == null || runMigration!.Equals("yes", StringComparison.CurrentCultureIgnoreCase)) &&
        databaseType != "inMemory")
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}

await app.RunAsync();