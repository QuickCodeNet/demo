using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using QuickCode.Demo.EmailManagerModule.Persistence.Contexts;
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
using Serilog;

Dictionary<string, string> environmentVariableConfigMap = new()
{
    { "READ_CONNECTION_STRING", "ConnectionStrings:ReadConnection" },
    { "WRITE_CONNECTION_STRING", "ConnectionStrings:WriteConnection" },
    { "ELASTIC_CONNECTION_STRING", "ConnectionStrings:ElasticConnection" },
    { "USERMANAGERMODULEAPIKEY" , "QuickCodeApiKeys:UserManagerModuleApiKey" },
	{ "SMSMANAGERMODULEAPIKEY" , "QuickCodeApiKeys:SmsManagerModuleApiKey" },
	{ "EMAILSENDERMODULEAPIKEY" , "QuickCodeApiKeys:EmailSenderModule" },
	{ "APIKEY" , "AppSettings:ApiKey" }
};

var builder = WebApplication.CreateBuilder(args);
var runMigration = Environment.GetEnvironmentVariable("RUN_MIGRATION");

builder.Configuration.UpdateConfigurationFromEnv(environmentVariableConfigMap);

var useHealthCheck = builder.Configuration.GetSection("AppSettings:UseHealthCheck").Get<bool>();
var databaseType = builder.Configuration.GetSection("AppSettings:DatabaseType").Get<string>();
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

builder.Services.AddLogger(builder.Configuration);
Log.Information($"Started({environmentName}) {builder.Configuration["Logging:ApiName"]} Started.");

builder.Services.AddMediatR<Program>();

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer(typeof(Program))));
    options.Filters.Add(typeof(ApiLogFilterAttribute));
    options.Filters.Add(new ProducesAttribute("application/json"));
}).AddJsonOptions(jsonOptions => { jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddResponseCompression();

builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();
builder.Services.AddQuickCodeDbContext<ReadDbContext, WriteDbContext>(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddQuickCodeRepositories(typeof(WriteDbContext));
builder.Services.AddQuickCodeSwaggerGen(builder.Configuration);
builder.Services.AddNswagServiceClient(builder.Configuration, typeof(Program));

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseResponseCompression();
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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHsts();
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