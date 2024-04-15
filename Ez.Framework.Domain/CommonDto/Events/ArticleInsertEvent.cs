using Ez.Infrastructure.LocalEventExtension;

namespace Ez.Domain.CommonDto.Events;

/// <summary>
/// </summary>
public class ArticleInsertEvent: IntegrationEvent
{
    /// <summary>
    /// ctor
    /// </summary>
    public ArticleInsertEvent()
    {
    }
    /// <summary></summary>
    public string EventData { get; init; } = string.Empty;

    /// <summary></summary>
    public override Guid Key { get; init; }
    /// <summary></summary>
    public override bool IsLog { get; init; }
}

