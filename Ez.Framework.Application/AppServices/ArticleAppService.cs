using Ez.Application.IAppServices;
using Ez.Domain.Entities;
using Ez.Domain.IRepositories;
namespace Ez.Application.AppServices;

/// <summary>
/// ArticleAppService
/// </summary>
public class ArticleAppService(IArticleRepository articleRepository):IArticleAppService
{
    /// <summary>
    /// insertData
    /// </summary>
    public async Task InsertDataAsync()
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
         await articleRepository.InsertAsync(article);
         await articleRepository.SaveChangesAsync();
    }
    
    /// <summary>
    /// GetArticleAsync
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    public async Task<Article> GetArticleAsync(Guid articleId)
    {
        //return await articleRepository.FirstOrDefaultAsNoTrackingAsync(x => x.ArticleId == articleId);
        return default;
    }
}
