namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public abstract record IntegrationEvent(Guid Id,bool IsLog):IIntegrationEvent
{
}