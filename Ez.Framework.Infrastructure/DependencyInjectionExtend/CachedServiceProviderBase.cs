using System.Collections.Concurrent;

namespace Ez.Infrastructure.DependencyInjectionExtend;

public abstract class CachedServiceProviderBase
{
    protected IServiceProvider ServiceProvider { get; }
    protected ConcurrentDictionary<Type, Lazy<object?>> CachedServices { get; }

    protected CachedServiceProviderBase(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        CachedServices = new ConcurrentDictionary<Type, Lazy<object?>>();
        CachedServices.TryAdd(typeof(IServiceProvider), new Lazy<object?>(() => ServiceProvider));
    }

    public virtual object? GetService(Type serviceType)
    {
        return CachedServices.GetOrAdd(
            serviceType,
            s => new Lazy<object?>(() => ServiceProvider.GetService(s))
        ).Value;
    }
}