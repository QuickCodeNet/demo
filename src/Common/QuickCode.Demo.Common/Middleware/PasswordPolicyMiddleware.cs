using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace QuickCode.Demo.Common.Middleware;

public class PasswordPolicyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PasswordPolicyMiddleware> _logger;

    public PasswordPolicyMiddleware(RequestDelegate next, ILogger<PasswordPolicyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsPasswordChangeRequest(context))
        {
            var password = await ExtractPasswordFromRequest(context);
            if (!string.IsNullOrEmpty(password))
            {
                var validationResult = ValidatePasswordStrength(password);
                if (!validationResult.IsValid)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Password does not meet security requirements",
                        details = validationResult.Errors
                    });
                    return;
                }
            }
        }

        await _next(context);
    }

    private static bool IsPasswordChangeRequest(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();
        return path.Contains("/password") || 
               path.Contains("/auth/change-password") ||
               path.Contains("/user/password");
    }

    private static async Task<string> ExtractPasswordFromRequest(HttpContext context)
    {
        if (context.Request.HasFormContentType)
        {
            var form = await context.Request.ReadFormAsync();
            return form["password"].ToString() ?? form["newPassword"].ToString() ?? string.Empty;
        }

        if (context.Request.ContentType?.Contains("application/json") == true)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            // Basit JSON parsing (production'da daha güvenli yöntem kullanın)
            if (body.Contains("password"))
            {
                var match = Regex.Match(body, @"""password""\s*:\s*""([^""]+)""");
                return match.Success ? match.Groups[1].Value : string.Empty;
            }
        }

        return string.Empty;
    }

    private PasswordValidationResult ValidatePasswordStrength(string password)
    {
        var errors = new List<string>();

        // Minimum uzunluk kontrolü
        if (password.Length < 12)
        {
            errors.Add("Password must be at least 12 characters long");
        }

        // Büyük harf kontrolü
        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            errors.Add("Password must contain at least one uppercase letter");
        }

        // Küçük harf kontrolü
        if (!Regex.IsMatch(password, @"[a-z]"))
        {
            errors.Add("Password must contain at least one lowercase letter");
        }

        // Rakam kontrolü
        if (!Regex.IsMatch(password, @"\d"))
        {
            errors.Add("Password must contain at least one number");
        }

        // Özel karakter kontrolü
        if (!Regex.IsMatch(password, @"[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]"))
        {
            errors.Add("Password must contain at least one special character");
        }

        // Yaygın şifre kontrolü
        var commonPasswords = new[]
        {
            "password", "123456", "qwerty", "admin", "letmein",
            "welcome", "monkey", "dragon", "master", "football"
        };

        if (commonPasswords.Any(cp => password.ToLower().Contains(cp)))
        {
            errors.Add("Password cannot contain common words or patterns");
        }

        // Ardışık karakter kontrolü
        if (Regex.IsMatch(password, @"(.)\1{2,}"))
        {
            errors.Add("Password cannot contain more than 2 consecutive identical characters");
        }

        return new PasswordValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }
}

public class PasswordValidationResult
{
    public bool IsValid { get; set; }
    public List<string> Errors { get; set; } = new();
}

public static class PasswordPolicyMiddlewareExtensions
{
    public static IApplicationBuilder UsePasswordPolicy(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<PasswordPolicyMiddleware>();
    }
} 