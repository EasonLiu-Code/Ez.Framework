using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Ez.Domain;
using Ez.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Persistence.AppDbContext;

namespace Persistence.Repositories;

public  class BaseRepository<TEntity>(ApplicationDbContext dbContext):IBaseRepository<TEntity> where TEntity:BaseEntity
{
    public async Task<TEntity?> FirstOrDefaultAsNoTrackingAsync(
        Expression<Func<TEntity?, bool>> func,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync<TEntity>(func!, cancellationToken);
    }
    
    public async Task<IList<TEntity>> GetAllListAsNoTrackingAsync(
        Expression<Func<TEntity, bool>>? func = null,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        var source =  dbContext.Set<TEntity>().AsNoTracking<TEntity>();
        List<TEntity> listAsync;
        if (func == null)
            listAsync = await source.ToListAsync<TEntity>(cancellationToken);
        else
            listAsync = await source.Where<TEntity>(func).ToListAsync<TEntity>(cancellationToken);
        return (IList<TEntity>) listAsync;
    }
    
    public  async Task<List<TEntity>> GetListAsync( 
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken=default
        )
    {
        return await dbContext.Set<TEntity>().AsQueryable().ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task<TEntity> InsertAsync(
        TEntity entity,
        bool autoSave = false,
        CancellationToken cancellationToken=default
    )
    {
        var savedEntity=await dbContext.Set<TEntity>().AddAsync(entity,cancellationToken);
        if (!autoSave)
        {
            var num = await dbContext.SaveChangesAsync(cancellationToken);
        }
        return savedEntity.Entity;
    }
    public async Task InsertManyAsync(
        IEnumerable<TEntity> entities,
        bool autoSave = false,
        CancellationToken cancellationToken=default)
    {
        var entityArray = entities.ToArray<TEntity>();
        await dbContext.Set<TEntity>().AddRangeAsync((IEnumerable<TEntity>)entityArray,cancellationToken);
        if (autoSave)
        {
            return;
        }
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<TEntity> UpdateAsync(
        TEntity entity,
        bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var updatedEntity = dbContext.Update<TEntity>(entity).Entity;
        if (!autoSave)
        {
            var num = await dbContext.SaveChangesAsync(cancellationToken);
        }
        TEntity entity1 = updatedEntity;
        updatedEntity = default (TEntity);
        return entity1;
    }
    public async Task UpdateManyAsync(
        IEnumerable<TEntity> entities,
        bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        foreach (TEntity entity1 in entities)
        {
            TEntity entity2 = await this.UpdateAsync(entity1, false, cancellationToken);
        }
        if (autoSave)
            return;
        await this.SaveChangesAsync(cancellationToken);
    }
    
    public  async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        var num = await dbContext.SaveChangesAsync(cancellationToken);
    }
}
