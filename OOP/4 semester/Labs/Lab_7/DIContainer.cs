namespace Lab_7;

public class DIContainer
{
    public interface IBox
    {
        public object GetObject { get; }
    }

    public class BoxScope : IDisposable, IBox
    {
        static BoxScope currentScope;
        BoxScope prevScope;
        object scopeObject;

        public BoxScope(object obj)
        {
            scopeObject = obj;
            prevScope = currentScope;
            currentScope = this;
        }
        public void Dispose()
        {
            currentScope = prevScope;
        }
        public object GetObject => currentScope.scopeObject;
    }

    public class BoxSingleton : IBox
    {
        static object obj;
        public BoxSingleton(Func<object> fabric)
        {
            obj ??= fabric();
        }
        public object GetObject => obj;
    }

    public class BoxPerRequest : IBox
    {
        Func<object> fabric;
        public BoxPerRequest(Func<object> fabric)
        {
            this.fabric = fabric;
        }
        public object GetObject => fabric();
    }


    Dictionary<Type, Func<IBox>> registrations = new Dictionary<Type, Func<IBox>>();

    public I GetInstance<I>()
    {
        return (I)GetInstance(typeof(I)).GetObject;
    }

    public IBox GetInstance(Type type)
    {
        if (!registrations.ContainsKey(type))
            throw new ArgumentException("Был получен незарегистрированный интерфейс");
        return registrations[type]();
    }

    public void Register<I, C>(LifeStyle lifeStyle) where C : class, I
    {
        switch (lifeStyle)
        {
            case LifeStyle.PerRequest:
                RegisterPerRequest<I, C>(Create<I, C>);
                break;
            case LifeStyle.Scoped:
                RegisterScoped<I, C>(Create<I, C>);
                break;
            case LifeStyle.Singleton:
                RegisterSingleton<I, C>(Create<I, C>);
                break;
        }
    }

    public void Register<I, C>(LifeStyle lifeStyle, Func<C> fabric) where C : class, I
    {
        switch (lifeStyle)
        {
            case LifeStyle.PerRequest:
                RegisterPerRequest<I, C>(fabric);
                break;
            case LifeStyle.Scoped:
                RegisterScoped<I, C>(fabric);
                break;
            case LifeStyle.Singleton:
                RegisterSingleton<I, C>(fabric);
                break;
        }
    }

    private C Create<I, C>() where C : class, I
    {
        var type = typeof(C);
        var constructor = type.GetConstructors().First();
        var parameters = constructor.GetParameters();
        var resolvedParams = parameters.Select(p => GetInstance(p.ParameterType).GetObject).ToArray();
        return (C)Activator.CreateInstance(type, resolvedParams);
    }

    private void RegisterPerRequest<I, C>(Func<C> fabric) where C : class, I
    {
        registrations[typeof(I)] = () => new BoxPerRequest(fabric);
    }

    private void RegisterScoped<I, C>(Func<C> fabric) where C : class, I
    {
        registrations[typeof(I)] = () => new BoxScope(fabric);
    }

    private void RegisterSingleton<I, C>(Func<C> fabric) where C : class, I
    {
        registrations[typeof(I)] = () => new BoxSingleton(fabric);
    }
}
