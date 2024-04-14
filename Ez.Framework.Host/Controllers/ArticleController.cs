
using Ez.Application.IAppServices;
using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ez.Framework.Controllers;

/// <summary>
/// ArticleController
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class ArticleController(IArticleAppService articleAppService):ControllerBase
{
    /// <summary>
    /// InsertDataAsync
    /// </summary>
    [HttpPost]
    public async Task InsertDataAsync()
    {
        await articleAppService.InsertDataAsync();
    }
    /// <summary>
    /// GetArticleAsync
    /// </summary>
    /// <param name="articleId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<Article> GetArticleAsync(Guid articleId)
    {
        return await articleAppService.GetArticleAsync(articleId);
    }

}