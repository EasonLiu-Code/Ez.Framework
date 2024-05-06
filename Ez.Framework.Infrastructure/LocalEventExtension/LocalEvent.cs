using System.Text.Json;
using StackExchange.Redis;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
internal sealed class LocalEvent(InMemoryMessageQueue queue):ILocalEvent
{
    private const string QueueKey = "integration_events";
    /// <summary>
    /// PublishAsync
    /// </summary>
    /// <param name="integrationEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task PublishAsync<T>(T integrationEvent,CancellationToken cancellationToken = default) where T : class,IIntegrationEvent
    {
        await queue.EnqueueAsync(integrationEvent);
    }
}