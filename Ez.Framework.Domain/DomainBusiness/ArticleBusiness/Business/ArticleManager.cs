using Ez.Domain.CommonDto.Events;
using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Ez.Domain.IRepositories;
using Ez.Infrastructure.LocalEventExtension;

namespace Ez.Domain.DomainBusiness.ArticleBusiness.Business;

/// <summary>
/// ArticleManager
/// </summary>
public class ArticleManager(
    IArticleRepository articleRepository,
    ILocalEvent localEvent)
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
        await localEvent.PublishAsync(new ArticleInsertEvent{EventData = "测试11111111111111111"},cancellationToken);
    }
}