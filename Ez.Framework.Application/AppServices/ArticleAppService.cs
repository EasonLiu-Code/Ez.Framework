using Ez.Application.IAppServices;
using Ez.Domain.DomainBusiness.ArticleBusiness.Business;
using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Ez.Domain.IRepositories;
using Ez.Infrastructure.LocalEventExtension;

namespace Ez.Application.AppServices;

/// <summary>
/// ArticleAppService
/// </summary>
public class ArticleAppService(
    IArticleRepository articleRepository,
    ArticleManager articleManager):IArticleAppService
{
    /// <summary>
    /// insertData
    /// </summary>
    public async Task InsertDataAsync()
    {
        await articleManager.InsertDataAsync();
    }
    
    /// <summary>
    /// GetArticleAsync
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    public async Task<Article> GetArticleAsync(Guid articleId)
    {
        return await articleRepository.FirstOrDefaultAsNoTrackingAsync(x => x.ArticleId.Equals(articleId));
    }
}
