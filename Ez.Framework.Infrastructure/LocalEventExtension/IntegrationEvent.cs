namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public abstract class IntegrationEvent:IIntegrationEvent
{
    /// <summary></summary>
    public abstract string? Key { get; init; }
    /// <summary></summary>
    public abstract bool IsLog { get; init; }
}