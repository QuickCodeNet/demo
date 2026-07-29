using Microsoft.Extensions.DependencyInjection;

namespace QuickCode.Demo.Gateway;

/// <summary>
/// User-owned DI registrations for the gateway. QuickCode never overwrites this file on regen.
/// </summary>
public static class SiteServiceRegistration
{
    public static IServiceCollection AddSiteCustomizations(this IServiceCollection services)
    {
        // Example: services.AddSingleton<IMyGatewayFilter, MyGatewayFilter>();
        return services;
    }
}
