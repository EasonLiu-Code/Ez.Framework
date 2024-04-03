using Ez.Domain.Entities;
using Ez.Domain.IRepositories;

namespace Persistence.Repositories;

internal abstract  class ArticleRepository(ApplicationDbContext dbContext) :BaseRepository<Article>(dbContext),IArticleRepository
{

}