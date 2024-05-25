using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary>
/// 
/// </summary>
internal sealed class IntegrationEventProcessor(
    InMemoryMessageQueue queue,
    IPublisher publisher,
    ILogger<IntegrationEventProcessor> logger) : BackgroundService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stoppingToken"></param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (IIntegrationEvent integrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            logger.LogInformation("Publishing {IntegrationEventId}", integrationEvent.Key);

            await publisher.Publish(integrationEvent, stoppingToken);

            logger.LogInformation("Processed {IntegrationEventId}", integrationEvent.Key);
        }
    }
}