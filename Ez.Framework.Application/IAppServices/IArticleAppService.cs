using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;

namespace Ez.Application.IAppServices;

/// <summary>
/// IArticleAppService
/// </summary>
public interface IArticleAppService
{
    /// <summary>
    /// InsertDataAsync
    /// </summary>
    /// <returns></returns>
    Task InsertDataAsync();

    /// <summary>
    /// GetArticleAsync
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    Task<Article> GetArticleAsync(Guid articleId);
}