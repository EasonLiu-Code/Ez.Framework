namespace Ez.Domain.Entities;

/// <summary>
/// Article
/// </summary>
public class Article:BaseEntity
{
    /// <summary>
    /// Title
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Content
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Tags
    /// </summary>
    public List<string> Tags{ get; set; } = new();

    /// <summary>
    /// CreatedOnUtc
    /// </summary>
    public DateTime CreatedOnUtc { get; set; }

    /// <summary>
    /// PublishedOnUtc
    /// </summary>
    public DateTime? PublishedOnUtc { get; set; }
}