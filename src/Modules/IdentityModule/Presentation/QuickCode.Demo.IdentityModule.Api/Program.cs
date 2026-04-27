using System;
using Dapper;
using System.Collections.Generic;
using System.IO.Compression;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using QuickCode.Demo.Common;
using QuickCode.Demo.Common.Data;
using QuickCode.Demo.Common.Filters;
using QuickCode.Demo.Common.Helpers;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Nswag.Extensions;
using QuickCode.Demo.Common.Extensions;
using QuickCode.Demo.IdentityModule.Api.Extension;
using QuickCode.Demo.IdentityModule.Persistence;
using QuickCode.Demo.IdentityModule.Persistence.Stores;
using QuickCode.Demo.Common.Mappers;
using Serilog;
using QuickCode.Demo.Common.Auditing;
using QuickCode.Demo.Common.Middleware;
using QuickCode.Demo.Common.ExceptionHandling;

DefaultTypeMap.MatchNamesWithUnderscores = true;
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.UpdateConfigurationFromEnv();

var runMigration = Environment.GetEnvironmentVariable("RUN_MIGRATION");
var useHealthCheck = builder.Configuration.GetSection("AppSettings:UseHealthCheck").Get<bool>();
var databaseType = builder.Configuration.GetSection("AppSettings:DatabaseType").Get<string>();
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

builder.Services.AddLogger(builder.Configuration);
Log.Information($"Started({environmentName}) {builder.Configuration["Logging:ApiName"]} Started.");

builder.Services.AddQuickCodeMediator<Program>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
    options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
}).AddJwtBearer(options => {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var secretKey = Environment.GetEnvironmentVariable("QUICKCODE_JWT_SECRET_KEY"); 
        var securityKey = Encoding.UTF8.GetBytes(secretKey!);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(securityKey)
        };
    })
    .AddCookie(IdentityConstants.ApplicationScheme, options =>
    {
        options.Cookie.Name = "QuickCodeCookieName";
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.AccessDeniedPath = "/api/auth/access-denied";
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new ToKebabParameterTransformer(typeof(Program))));
    options.Filters.Add(typeof(ApiLogFilterAttribute));
    options.Filters.Add(new ProducesAttribute("application/json"));
}).AddJsonOptions(jsonOptions => { jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddIdentityCore<ApiUser>()
    .AddUserStore<ApiUserDapperStore>()
    .AddDefaultTokenProviders()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>()
    .AddSignInManager()
    .AddApiEndpoints();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

builder.Services.AddResponseCompression();
builder.Services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
builder.Services.AddSingleton<ApiKeyAuthorizationFilter>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddBankingGradeAuditing(builder.Configuration);

builder.Services.AddQuickCodeDbConnectionFactory();
builder.Services.AddHealthChecks();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddQuickCodeRepositories("QuickCode.Demo.IdentityModule.Persistence", "QuickCode.Demo.IdentityModule.Application");
builder.Services.AddQuickCodeSwaggerGen(builder.Configuration);
builder.Services.AddNswagServiceClient(builder.Configuration, typeof(Program));
builder.Services.AddServiceClient(builder.Configuration, typeof(Program));
builder.Services.AddEndpointsApiExplorer();

DapperTypeMapper.ConfigureTypeMappings();

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

app.MapGroup("/api/auth").WithTags("Authentications").MapQuickCodeIdentityApi<ApiUser>();

app.MapControllers();

if (useHealthCheck && databaseType != "inMemory")
{
    app.UseHealthChecks("/hc", new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
}

app.UseExceptionHandler();

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
        var migrationRunner = scope.ServiceProvider.GetRequiredService<QuickCodeSqlMigrationRunner>();
        await migrationRunner.MigrateAsync();
        
        var seedRunner = scope.ServiceProvider.GetRequiredService<QuickCodeSeedDataRunner>();
        await seedRunner.SeedAsync();
    }
}

await app.RunAsync();