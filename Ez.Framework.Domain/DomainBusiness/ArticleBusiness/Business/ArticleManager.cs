using Ez.Domain.CommonDto.Events;
using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Ez.Domain.IRepositories;
using Ez.Infrastructure.LocalEventExtension;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace Ez.Domain.DomainBusiness.ArticleBusiness.Business;

/// <summary>
/// ArticleManager
/// </summary>
public class ArticleManager(
    IArticleRepository articleRepository,
    ILocalEvent localEvent,
    IDistributedCache distributedCache,
    ILogger<ArticleManager> logger)
{
    /// <summary>
    /// InsertDataAsync
    /// </summary>
    public async Task InsertDataAsync(CancellationToken cancellationToken=default)
    {
        var article = new Article
        {
            Title = "test",
            Content = "test",
            CreatedOnUtc= DateTime.UtcNow.Date,
            Tags = new List<string> {"test"},
            ArticleId = Guid.NewGuid(),
            PublishedOnUtc = DateTime.UtcNow.Date
        };
        await articleRepository.InsertAsync(article,cancellationToken:cancellationToken);
        await articleRepository.SaveChangesAsync(cancellationToken:cancellationToken);
        logger.LogInformation("successful:{name}", article.ArticleId);
        await localEvent.PublishAsync(new ArticleInsertEvent(eventData:"111",key:$"{InsertDataAsync}-{DateTime.Now}"),cancellationToken);
    }
}