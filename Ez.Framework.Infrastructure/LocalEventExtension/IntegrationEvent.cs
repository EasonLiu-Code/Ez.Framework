namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public abstract class IntegrationEvent:IIntegrationEvent
{
    /// <summary></summary>
    public Guid Id { get; init; }
    /// <summary></summary>
    public bool IsLog { get; init; }
}