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
        this.Id= Guid.NewGuid();
        this.IsLog = false;
    }
    /// <summary></summary>
    public string EventData { get; init; } = string.Empty;
    
}

