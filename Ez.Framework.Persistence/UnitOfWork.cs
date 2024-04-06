using Persistence.AppDbContext;

namespace Persistence;

internal sealed class UnitOfWork(ApplicationDbContext dbContext)
{
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}