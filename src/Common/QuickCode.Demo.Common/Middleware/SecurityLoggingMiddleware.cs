using Microsoft.AspNetCore.Http;

namespace QuickCode.Demo.Common.Middleware;

public class SecurityLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SecurityLoggingMiddleware> _logger;

    public SecurityLoggingMiddleware(RequestDelegate next, ILogger<SecurityLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        var originalStatusCode = context.Response.StatusCode;

        try
        {
            await _next(context);
        }
        finally
        {
            await LogSecurityEvents(context, startTime, originalStatusCode);
        }
    }

    private async Task LogSecurityEvents(HttpContext context, DateTime startTime, int originalStatusCode)
    {
        var duration = DateTime.UtcNow - startTime;
        var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var userAgent = context.Request.Headers["User-Agent"].ToString();
        var path = context.Request.Path.ToString();
        var method = context.Request.Method;
        var statusCode = context.Response.StatusCode;

        // Log authentication failures
        if (statusCode == 401)
        {
            _logger.LogWarning("Authentication failure: {Method} {Path} from {IP} - Status: {StatusCode}", 
                method, path, clientIp, statusCode);
        }

        // Log authorization failures
        if (statusCode == 403)
        {
            _logger.LogWarning("Authorization failure: {Method} {Path} from {IP} - Status: {StatusCode}", 
                method, path, clientIp, statusCode);
        }

        // Log potential attacks
        if (IsPotentialAttack(context))
        {
            _logger.LogError("Potential security attack detected: {Method} {Path} from {IP} - UserAgent: {UserAgent}", 
                method, path, clientIp, userAgent);
        }

        // Log suspicious patterns
        if (ContainsSuspiciousPatterns(context))
        {
            _logger.LogWarning("Suspicious request pattern: {Method} {Path} from {IP}", 
                method, path, clientIp);
        }

        // Log high-frequency requests (potential DDoS)
        if (duration.TotalMilliseconds > 5000) // Requests taking more than 5 seconds
        {
            _logger.LogWarning("Slow request detected: {Method} {Path} from {IP} - Duration: {Duration}ms", 
                method, path, clientIp, duration.TotalMilliseconds);
        }
    }

    private static bool IsPotentialAttack(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();
        var suspiciousPaths = new[]
        {
            "/admin", "/wp-admin", "/phpmyadmin", "/config", "/.env",
            "/api/users", "/api/admin", "/api/config", "/api/secrets"
        };

        return suspiciousPaths.Any(sp => path.Contains(sp)) && 
               !context.User.Identity?.IsAuthenticated == true;
    }

    private static bool ContainsSuspiciousPatterns(HttpContext context)
    {
        var queryString = context.Request.QueryString.ToString().ToLower();
        var suspiciousPatterns = new[]
        {
            "union select", "drop table", "delete from", "insert into",
            "script", "javascript:", "onload=", "onerror="
        };

        return suspiciousPatterns.Any(pattern => queryString.Contains(pattern));
    }
}

public static class SecurityLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseSecurityLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecurityLoggingMiddleware>();
    }
} 