using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using QuickCode.Demo.Common.Auditing;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Swashbuckle.AspNetCore.SwaggerGen;
using QuickCode.Demo.Common.Models;
using QuickCode.Demo.Common.Data;

namespace QuickCode.Demo.Common;

public static class ServiceCollectionExtensions
{
    private const string ElasticSearchUriKey = "Logging:ElasticConfiguration:Uri";

    public static IServiceCollection AddQuickCodeSwaggerGen(this IServiceCollection services,IConfiguration configuration, Action<SwaggerGenOptions>? configureOptions=null, string tokenType = "ApiKey")
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
            options.EnableAnnotations();
            options.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = $"{configuration["Swagger:Title"]} ({Assembly.GetExecutingAssembly().GetName().Version})",
                TermsOfService = new Uri(configuration["Swagger:TermsOfService"]!),
                Contact = new OpenApiContact
                {
                    Name = "Üzeyir Apaydın",
                    Url = new Uri(configuration["Swagger:Contact"]!)
                },
                License = new OpenApiLicense
                {
                    Name = "QuickCode.Net License",
                    Url = new Uri(configuration["Swagger:License"]!)
                }
            });

            options.CustomOperationIds(e =>
            {
                try
                {
                    var controllerName = e.ActionDescriptor.RouteValues["controller"];
                    var actionName = e.ActionDescriptor.RouteValues["action"];
                    var operationId = $"{controllerName}{actionName}";
                    return operationId;
                }
                catch
                {
                    var relativePath = e.RelativePath!.Replace("/", "-")
                        .Replace("{", "-")
                        .Replace("}", "-");
                    var operationId = $"{relativePath}-{e.HttpMethod!.ToLowerInvariant()}".Replace("--", "-");
                    return operationId;
                }
            });
            
            switch (tokenType)
            {
                case "Bearer":
                    options.AddSecurityDefinition(tokenType, new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = tokenType,
                        Scheme = tokenType
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = tokenType
                                },
                            },
                            new List<string>() { }
                        }
                    });
                    break;
                case "ApiKey":
                    options.AddSecurityDefinition(tokenType, new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid Api Key",
                        Name = "X-Api-Key",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = $"{tokenType}Schema"
                    });
                    
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = tokenType
                                },
                            },
                            new List<string>() { }
                        }
                    });
                    break;
            }

            configureOptions?.Invoke(options);

            options.SchemaFilter<EnumSchemaFilter>();
        });

        return services;
    }

    public static IServiceCollection AddQuickCodeDbConnectionFactory(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<QuickCodeSqlMigrationRunner>();
        services.AddSingleton<QuickCodeSeedDataRunner>();
        return services;
    }
    
    public static void AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var excludingFilters = new Dictionary<string, List<string>>()
            {
                { "RequestPath", ["/hc"] }
            };

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .Filter.ByExcluding(e =>
                    excludingFilters.Keys.Any(filterKey =>
                        e.Properties.ContainsKey(filterKey) &&
                        excludingFilters[filterKey].Contains(((ScalarValue)e.Properties[filterKey]).Value!.ToString()!)))
                .ReadFrom.Configuration(configuration)
                .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment!))
                .Enrich.WithProperty("Environment", environment!)
                .CreateLogger();

            services.AddSerilog(logger);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
    {
        var elasticConnectionString = Environment.GetEnvironmentVariable("ELASTIC_CONNECTION_STRING") ??
                                      configuration[ElasticSearchUriKey]!;
        
        return new ElasticsearchSinkOptions(new Uri(elasticConnectionString))
        {
            AutoRegisterTemplate = true,
            IndexFormat =
                $"{configuration["Logging:ApiName"]}--{environment?.ToLower()
                    .Replace(".", "-")}--{DateTime.UtcNow:yyyy-MM-dd}"
        };
    }
}
