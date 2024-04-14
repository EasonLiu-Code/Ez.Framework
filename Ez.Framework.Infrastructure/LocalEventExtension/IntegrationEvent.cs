namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public abstract record IntegrationEvent(Guid Id):IIntegrationEvent
{
    /// <summary></summary>
    public Guid Id { get; init; }
}