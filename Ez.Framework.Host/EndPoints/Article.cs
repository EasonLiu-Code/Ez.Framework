using Carter;
using Ez.Application.IAppServices;
using Persistence.ConstStrings;

namespace Ez.Framework.EndPoints;

/// <summary>
/// Article
/// </summary>
public class Article(IArticleAppService articleAppService):ICarterModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet(ConstStrings.ArticleApi,async (Guid articleId)=>await articleAppService.GetArticleAsync(articleId)).WithName("GetArticle");
    }
}