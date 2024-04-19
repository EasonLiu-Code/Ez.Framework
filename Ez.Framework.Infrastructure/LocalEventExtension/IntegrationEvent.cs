namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public abstract class IntegrationEvent:IIntegrationEvent
{
    /// <summary></summary>
    public abstract string Key { get; init; }
}