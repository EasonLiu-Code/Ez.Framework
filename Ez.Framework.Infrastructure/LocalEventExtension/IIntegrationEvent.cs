using MediatR;

namespace Ez.Infrastructure.LocalEventExtension;

/// <summary></summary>
public interface IIntegrationEvent:INotification
{
    /// <summary></summary>
    Guid Id { get; init; }

    /// <summary>
    ///是否日志记录
    /// </summary>
    public bool IsLog { get; set; }
}