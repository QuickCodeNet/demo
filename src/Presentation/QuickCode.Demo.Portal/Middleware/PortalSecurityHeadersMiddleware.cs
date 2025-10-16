using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace QuickCode.Demo.Portal.Middleware;

public class PortalSecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public PortalSecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // OWASP A02:2021 - Cryptographic Failures
        context.Response.Headers.TryAdd("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
        
        // OWASP A05:2021 - Security Misconfiguration
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
        context.Response.Headers.TryAdd("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
        context.Response.Headers.TryAdd("Permissions-Policy", "geolocation=(), microphone=(), camera=(), payment=()");
        
        context.Response.Headers.TryAdd("Content-Security-Policy", 
            "default-src 'self'; " +
            "script-src 'self' 'unsafe-inline' 'unsafe-eval' " +
            "https://cdnjs.cloudflare.com " +
            "https://cdn.jsdelivr.net " +
            "https://gitcdn.github.io " +
            "https://code.jquery.com " +
            "https://cdnjs.cloudflare.com/ajax/libs/ace/1.18.0/ " +
            "https://cdnjs.cloudflare.com/ajax/libs/jsoneditor/9.10.0/ " +
            "https://cdn.jsdelivr.net/npm/chart.js@4.0.1/dist/ " +
            "https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/ " +
            "https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/; " +
            "style-src 'self' 'unsafe-inline' " +
            "https://fonts.googleapis.com " +
            "https://cdnjs.cloudflare.com " +
            "https://gitcdn.github.io " +
            "https://gitcdn.github.io/bootstrap-toggle/2.2.2/css/ " +
            "https://cdn.jsdelivr.net/npm/summernote@0.8.18/dist/ " +
            "https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/; " +
            "font-src 'self' data: " +
            "https://fonts.gstatic.com " +
            "https://cdnjs.cloudflare.com " +
            "https://cdn.jsdelivr.net; " +
            "img-src 'self' data: https:; " +
            "connect-src 'self'; " +
            "frame-src 'self' http://localhost:* https://localhost:* https://*.quickcode.net https://*.europe-west1.run.app; " +
            "frame-ancestors 'none';");
        
        // Additional security headers
        context.Response.Headers.TryAdd("X-Permitted-Cross-Domain-Policies", "none");
        context.Response.Headers.TryAdd("X-Download-Options", "noopen");
        
        await _next(context);
    }
}

public static class PortalSecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UsePortalSecurityHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PortalSecurityHeadersMiddleware>();
    }
} 