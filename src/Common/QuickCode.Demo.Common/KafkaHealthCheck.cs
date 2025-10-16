using Confluent.Kafka;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace QuickCode.Demo.Common;

public class KafkaHealthCheck : IHealthCheck
{
    private readonly string _bootstrapServers;

    public KafkaHealthCheck(string bootstrapServers)
    {
        _bootstrapServers = bootstrapServers;
    }
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var metadata = await Task.Run(() =>
            {
                using var adminClient =
                    new AdminClientBuilder(new AdminClientConfig
                    {
                        BootstrapServers = _bootstrapServers
                    }).Build();
                
                return adminClient.GetMetadata(TimeSpan.FromSeconds(10));
            }, cancellationToken);

            return HealthCheckResult.Healthy(
                $"Successfully connected to Kafka. Found {metadata.Brokers.Count} broker(s).");
        }
        catch (KafkaException ex)
        {
            return HealthCheckResult.Unhealthy($"Unable to connect to Kafka: {ex.Message}", ex);
        }
    }
}
