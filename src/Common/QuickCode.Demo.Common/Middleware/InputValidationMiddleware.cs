using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace QuickCode.Demo.Common.Middleware;

public class InputValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<InputValidationMiddleware> _logger;

    public InputValidationMiddleware(RequestDelegate next, ILogger<InputValidationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Validate query parameters
            foreach (var param in context.Request.Query)
            {
                if (ContainsSuspiciousContent(param.Value.ToString()))
                {
                    _logger.LogWarning("Suspicious input detected in query parameter: {ParamName}", param.Key);
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input detected");
                    return;
                }
            }

            // Validate form data
            if (context.Request.HasFormContentType)
            {
                var form = await context.Request.ReadFormAsync();
                foreach (var field in form)
                {
                    if (ContainsSuspiciousContent(field.Value.ToString()))
                    {
                        _logger.LogWarning("Suspicious input detected in form field: {FieldName}", field.Key);
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid input detected");
                        return;
                    }
                }
            }

            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in input validation middleware");
            throw;
        }
    }

    private static bool ContainsSuspiciousContent(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        // SQL Injection patterns
        var sqlPatterns = new[]
        {
            @"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|CREATE|ALTER|EXEC|UNION)\b)",
            @"(--|/\*|\*/)",
            @"(\b(OR|AND)\b\s+\d+\s*=\s*\d+)",
            @"(\b(OR|AND)\b\s+['""]\w+['""]\s*=\s*['""]\w+['""])"
        };

        // XSS patterns
        var xssPatterns = new[]
        {
            @"<script[^>]*>.*?</script>",
            @"javascript:",
            @"on\w+\s*=",
            @"<iframe[^>]*>",
            @"<object[^>]*>",
            @"<embed[^>]*>"
        };

        var allPatterns = sqlPatterns.Concat(xssPatterns);

        return allPatterns.Any(pattern => Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase));
    }
}

public static class InputValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseInputValidation(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<InputValidationMiddleware>();
    }
} 