using Microsoft.Extensions.DependencyInjection;

namespace Ez.Application;

/// <summary>
/// DependencyInjection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// di
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}