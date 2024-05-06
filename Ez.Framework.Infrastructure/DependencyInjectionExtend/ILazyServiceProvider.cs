namespace Ez.Infrastructure.DependencyInjectionExtend;

public interface ILazyServiceProvider
{
    T LazyGetRequiredService<T>();

    object LazyGetRequiredService(Type serviceType);

    T? LazyGetService<T>();

    object? LazyGetService(Type serviceType);

    T LazyGetService<T>(T defaultValue) where T : notnull;

    object LazyGetService(Type serviceType, object defaultValue);

    object? LazyGetService(Type serviceType, Func<IServiceProvider, object> factory);

    T? LazyGetService<T>(Func<IServiceProvider, object> factory);
}