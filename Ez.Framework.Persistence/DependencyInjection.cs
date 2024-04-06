using Ez.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.AppDbContext;
using Persistence.Repositories;
namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseNpgsql(configuration.GetConnectionString("Database")));
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        return services;
    }
}