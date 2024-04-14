namespace Ez.Domain.DomainBusiness.ArticleBusiness.Entities;

/// <summary>
/// Article
/// </summary>
public class Article:BaseEntity
{
    
    /// <summary>
    /// ArticleId
    /// </summary>
    public Guid  ArticleId { get; set; }

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