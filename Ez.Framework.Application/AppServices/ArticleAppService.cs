using Ez.Application.IAppServices;
using Ez.Domain.DomainBusiness.ArticleBusiness.Business;
using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Ez.Domain.IRepositories;
using Ez.Infrastructure.DependencyInjectionExtend;
using Ez.Infrastructure.LocalEventExtension;

namespace Ez.Application.AppServices;

/// <summary>
/// ArticleAppService
/// </summary>
public class ArticleAppService:IArticleAppService
{
    private readonly ILazyServiceProvider LazyServiceProvider;
    private ArticleManager ArticleManager=>LazyServiceProvider.LazyGetRequiredService<ArticleManager>();
    private IArticleRepository ArticleRepository=>LazyServiceProvider.LazyGetRequiredService<IArticleRepository>();
    /// <summary>
    /// 
    /// </summary>
    public ArticleAppService(ILazyServiceProvider lazyServiceProvider)
    {
        LazyServiceProvider = lazyServiceProvider;
    }
    /// <summary>
    /// insertData
    /// </summary>
    public async Task InsertDataAsync()
    {
        await ArticleManager.InsertDataAsync();
    }
    /// <summary>
    /// GetArticleAsync
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    public async Task<Article> GetArticleAsync(Guid articleId)
    {
        return await ArticleRepository.FirstOrDefaultAsNoTrackingAsync(x => x.ArticleId.Equals(articleId));
    }
}
