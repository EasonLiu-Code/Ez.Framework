using Ez.Infrastructure.LocalEventExtension;

namespace Ez.Domain.CommonDto.Events;

/// <summary>
/// </summary>
public class ArticleInsertEvent: IntegrationEvent
{
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleInsertEvent(string key, string eventData)
    {
        Key = key;
        EventData = eventData;
    }
    /// <summary></summary>
    public string EventData { get; init; }
    /// <summary></summary>
    public sealed override string? Key { get; init; }
}

