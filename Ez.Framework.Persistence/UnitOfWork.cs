namespace Persistence;

internal sealed class UnitOfWork(ApplicationDbContext _dbContext)
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }
}