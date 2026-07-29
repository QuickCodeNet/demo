using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using QuickCode.Demo.Infrastructure.Web.Extensions;
using QuickCode.Demo.Infrastructure.Web.Helpers;
using QuickCode.Demo.Portal.Helpers.BlobStorage;

namespace QuickCode.Demo.Portal;

/// <summary>
/// User-owned DI registrations for the portal. QuickCode never overwrites this file on regen.
/// </summary>
public static class SiteServiceRegistration
{
    public static IServiceCollection AddSiteCustomizations(this IServiceCollection services)
    {
        // BLOB_PROVIDER=azure|r2|minio|local (default: azure)
        services.AddBlobImageStorage();

        // AJAX/XHR auth challenges must return JSON (with redirectUrl), not a 302 login HTML page.
        services.PostConfigure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.Events.OnRedirectToLogin = async context =>
            {
                if (context.Request.WantsJsonResponse())
                {
                    await JsonErrorResponseWriter.WriteAsync(
                        context.HttpContext,
                        StatusCodes.Status401Unauthorized,
                        "Your session has expired. Please log in again.",
                        "/Login/Index");
                    return;
                }

                context.Response.Redirect(context.RedirectUri);
            };

            options.Events.OnRedirectToAccessDenied = async context =>
            {
                if (context.Request.WantsJsonResponse())
                {
                    await JsonErrorResponseWriter.WriteAsync(
                        context.HttpContext,
                        StatusCodes.Status403Forbidden,
                        "You do not have permission to perform this action.");
                    return;
                }

                context.Response.Redirect(context.RedirectUri);
            };
        });

        return services;
    }
}
