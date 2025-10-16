using QuickCode.Demo.Common.Extensions;
using Serilog;

namespace QuickCode.Demo.Common.Helpers;
public static class EnvironmentHelper
{
    public static void UpdateConfigurationFromEnv(this IConfiguration configuration)
    {
        var envVariables = Environment.GetEnvironmentVariables();

        foreach (var key in envVariables.Keys)
        {
            var envKey = key.ToString();
            if (string.IsNullOrEmpty(envKey))
            {
                continue;
            }

            var envValue = envVariables[key]?.ToString();
            configuration.SetConfigValue($"QuickcodeApiKeys:{envKey.GetPascalCase()}", envValue!);
            configuration.SetConfigValue($"ConnectionStrings:{envKey.GetPascalCase()}", envValue!);
        }
    }

    private static void SetConfigValue(this IConfiguration configuration, string key, string value)
    {
        if (configuration[key] == null)
        {
            return;
        }
        
        configuration[key] = value;
        Log.Information("Config Key:{Key} updated from environment variable", key);
    }
}