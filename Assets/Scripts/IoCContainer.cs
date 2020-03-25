using System;
using Microsoft.MinIoC;

internal static class IoCContainer
{
    private static readonly Container _container;

    static IoCContainer()
    {
        _container = new Container();
        Register(() => new PreloadStateMachineController()).AsSingleton();
    }

    public static Container.IRegisteredType Register<T>(Type type) => _container.Register(typeof(T), type);

    public static Container.IRegisteredType Register<TInterface, TImplementation>() =>
        _container.Register(typeof(TInterface), typeof(TImplementation));

    public static Container.IRegisteredType Register<T>(Func<T> factory) =>
        _container.Register(typeof(T), () => factory());

    public static Container.IRegisteredType Register<T>() => _container.Register(typeof(T), typeof(T));

    public static T Resolve<T>() => _container.Resolve<T>();
}