namespace Ez.Infrastructure.DependencyInjectionExtend;

public class LazyServiceProvider(IServiceProvider serviceProvider)
    : CachedServiceProviderBase(serviceProvider), ILazyServiceProvider
{
    public virtual T LazyGetRequiredService<T>()
    {
        return (T)LazyGetRequiredService(typeof(T));
    }

    public virtual object LazyGetRequiredService(Type serviceType)
    {
        return GetService(serviceType)!;
    }

    public virtual T? LazyGetService<T>()
    {
        return (T?)LazyGetService(typeof(T));
    }

    public virtual object? LazyGetService(Type serviceType)
    {
        return GetService(serviceType);
    }

    public virtual T LazyGetService<T>(T defaultValue) where T : notnull
    {
        return (T)LazyGetService(typeof(T), defaultValue);
    }

    public virtual object LazyGetService(Type serviceType, object defaultValue)
    {
        return LazyGetService(serviceType) ?? defaultValue;
    }

    public virtual T? LazyGetService<T>(Func<IServiceProvider, object> factory)
    {
        return (T?)LazyGetService(typeof(T), factory);
    }

    public virtual object? LazyGetService(Type serviceType, Func<IServiceProvider, object?> factory)
    {
        return CachedServices.GetOrAdd(
            serviceType,
            _ => new Lazy<object?>(() => factory(ServiceProvider))
        ).Value;
    }
}