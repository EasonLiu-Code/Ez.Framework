namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public abstract record IntegrationEvent(Guid Id,bool IsLog):IIntegrationEvent
{
    /// <summary></summary>
    public Guid Id { get; init; }

    /// <summary></summary>
    public bool IsLog { get; set; } = false;
}