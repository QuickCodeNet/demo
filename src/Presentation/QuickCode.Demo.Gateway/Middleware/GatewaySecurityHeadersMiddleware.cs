using Microsoft.AspNetCore.Http;

namespace QuickCode.Demo.Gateway.Middleware;

public class GatewaySecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public GatewaySecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // OWASP A02:2021 - Cryptographic Failures
        context.Response.Headers.TryAdd("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
        
        // OWASP A05:2021 - Security Misconfiguration
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Response.Headers.TryAdd("X-Frame-Options", "SAMEORIGIN"); 
        context.Response.Headers.TryAdd("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
        context.Response.Headers.TryAdd("Permissions-Policy", "geolocation=(), microphone=(), camera=(), payment=()");
        
        context.Response.Headers.TryAdd("Content-Security-Policy", 
            "default-src 'self'; " +
            "script-src 'self' 'unsafe-inline' 'unsafe-eval' " +
            "https://ajax.googleapis.com " +
            "https://cdnjs.cloudflare.com " +
            "https://cdn.jsdelivr.net " +
            "https://gitcdn.github.io " +
            "https://code.jquery.com; " +
            "style-src 'self' 'unsafe-inline' " +
            "https://fonts.googleapis.com " +
            "https://cdnjs.cloudflare.com " +
            "https://gitcdn.github.io " +
            "https://cdn.jsdelivr.net; " +
            "font-src 'self' data: " +
            "https://fonts.gstatic.com " +
            "https://cdnjs.cloudflare.com " +
            "https://cdn.jsdelivr.net; " +
            "img-src 'self' data: https:; " +
            "connect-src 'self'; " +
            "frame-src 'self' http://localhost:* https://localhost:* https://*.quickcode.net https://*.europe-west1.run.app; " +
            "frame-ancestors 'self';"); // For Swagger iframe 
        
        // Additional security headers
        context.Response.Headers.TryAdd("X-Permitted-Cross-Domain-Policies", "none");
        context.Response.Headers.TryAdd("X-Download-Options", "noopen");
        
        await _next(context);
    }
}

public static class GatewaySecurityHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseGatewaySecurityHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GatewaySecurityHeadersMiddleware>();
    }
} 