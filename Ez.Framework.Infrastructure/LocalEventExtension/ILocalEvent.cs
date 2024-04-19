namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public interface ILocalEvent
{
    /// <summary>
    /// PublishAsync
    /// </summary>
    /// <param name="integrationEvent"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task PublishAsync<T>(T integrationEvent,CancellationToken cancellationToken = default)
        where T : class, IIntegrationEvent;
}