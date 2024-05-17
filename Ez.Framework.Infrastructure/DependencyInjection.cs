using Ez.Infrastructure.DependencyInjectionExtend;
using Ez.Infrastructure.LocalEventExtension;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ez.Infrastructure;

/// <summary></summary>
public static class DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddSingleton<InMemoryMessageQueue>();
        services.AddSingleton<ILocalEvent, LocalEvent>();
        services.AddHostedService<IntegrationEventProcessor>();
        services.AddTransient<ILazyServiceProvider, LazyServiceProvider>();
        return services;
    }
}