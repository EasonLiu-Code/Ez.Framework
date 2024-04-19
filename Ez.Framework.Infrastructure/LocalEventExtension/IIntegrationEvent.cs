using MediatR;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public interface IIntegrationEvent:INotification
{
    /// <summary></summary>
    string Key { get; init; }
}