using Ez.Domain.DomainBusiness.ArticleBusiness.Entities;
using Ez.Domain.IRepositories;
using Persistence.AppDbContext;

namespace Persistence.Repositories;

public  class ArticleRepository(ApplicationDbContext dbContext) :BaseRepository<Article>(dbContext),IArticleRepository
{

}