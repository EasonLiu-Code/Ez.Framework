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
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var integrationEvent = await queue.DequeueAsync<IIntegrationEvent>();
                await publisher.Publish(integrationEvent, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing integration event");
            }
        }
    }
}