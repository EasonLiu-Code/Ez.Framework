using Ez.Application.AppServices;
using Ez.Application.IAppServices;
using Microsoft.Extensions.DependencyInjection;

namespace Ez.Application;

/// <summary>
/// DependencyInjection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// AddApplication
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleAppService, ArticleAppService>();
        return services;
    }
}