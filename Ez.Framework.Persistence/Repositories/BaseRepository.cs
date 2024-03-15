using Ez.Domain;
using Ez.Domain.Entities;

namespace Persistence.Repositories;

internal abstract class BaseRepository<TEntity>(ApplicationDbContext dbContext):IBaseRepository<TEntity> where TEntity:BaseEntity
{
}