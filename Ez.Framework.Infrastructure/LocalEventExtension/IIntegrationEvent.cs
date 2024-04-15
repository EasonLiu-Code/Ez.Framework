using MediatR;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public interface IIntegrationEvent:INotification
{
    /// <summary></summary>
    Guid Id { get; init; }

    /// <summary></summary>
    public bool IsLog { get; init; }
}