using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Ez.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;

namespace Persistence.Repositories;

public  class ArticleRepository(ApplicationDbContext dbContext) :BaseRepository<Article>(dbContext),IArticleRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    /// <summary>
    /// 批量更新
    /// ExecuteUpdateAsync速度快,不跟踪无需SaveChanges开启事务操作可以保证一致性方便回滚。
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancellationToken"></param>
    public async Task BulkUpdatesAsync(
        string title,
        CancellationToken cancellationToken = default)
    {
        await using var transaction= await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await _dbContext.Articles
                .Where(x => title.Contains(x.Title))
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.Tags, new List<string> { "测试", "Test" }),
                    cancellationToken);
            // ... other operations that modify entities
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
        }
    }
}