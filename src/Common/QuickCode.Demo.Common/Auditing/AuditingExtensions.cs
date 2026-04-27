using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QuickCode.Demo.Common.Auditing;

public static class AuditingExtensions
{
    /// <summary>
    /// Registers <see cref="IAuditLogWriter"/> as a no-op for hosts (Portal, Gateway) that run
    /// security audit middleware but do not use a write database connection.
    /// </summary>
    public static IServiceCollection AddNoOpAuditLogWriter(this IServiceCollection services)
    {
        services.AddSingleton<IAuditLogWriter, NullAuditLogWriter>();
        return services;
    }

    public static IServiceCollection AddBankingGradeAuditing(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AuditingOptions>(
            configuration.GetSection(AuditingOptions.SectionName));
        
        services.AddSingleton<IAuditLogWriter, BackgroundAuditLogWriter>();
        services.AddHostedService(provider => 
            (BackgroundAuditLogWriter)provider.GetRequiredService<IAuditLogWriter>());
        
        return services;
    }
}

