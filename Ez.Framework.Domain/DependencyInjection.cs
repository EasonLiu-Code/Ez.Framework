using Ez.Domain.DomainBusiness.ArticleBusiness.Business;
using Microsoft.Extensions.DependencyInjection;

namespace Ez.Domain;

/// <summary>
/// DependencyInjection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// AddDomain
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ArticleManager>();
        return services;
    }
}