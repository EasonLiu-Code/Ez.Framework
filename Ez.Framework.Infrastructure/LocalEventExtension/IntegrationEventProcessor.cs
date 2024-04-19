using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary>
/// 
/// </summary>
internal sealed class IntegrationEventProcessor(
    InMemoryMessageQueue queue,
    IPublisher publisher,
    IConnectionMultiplexer redis,
    ILogger<IntegrationEventProcessor> logger) : BackgroundService
{
    private const string QueueKey = "integration_events";
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(async () =>
        {
            while (true)
            {
                var eventData = await redis.GetDatabase().ListLeftPopAsync(QueueKey);
                if (!eventData.HasValue) continue;
                IIntegrationEvent integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(eventData);
                if (integrationEvent != null) await queue.Writer.WriteAsync(integrationEvent, stoppingToken);
            }
        },stoppingToken);
        await foreach (IIntegrationEvent integrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                await publisher.Publish(integrationEvent, stoppingToken);
            }
            catch
            {
                logger.LogInformation("Error {IntegrationEventKey}", integrationEvent.Key);
            }
        }
    }
}