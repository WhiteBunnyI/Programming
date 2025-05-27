namespace Lab_7;

public class DIContainer : IDisposable
{
    public interface IBox
    {
        public object GetObject { get; }
    }

    public class BoxScope : IBox
    {
        static BoxScope currentScope;
        BoxScope prevScope;
        object scopeObject;

        Func<object> fabric;
        public BoxScope(Func<object> fabric)
        {
            this.fabric = fabric;
            scopeObject = fabric();
            prevScope = currentScope;
            currentScope = this;
        }

        public BoxScope CreateScope()
        {
            return new BoxScope(fabric);
        }

        public BoxScope Dispose()
        {
            currentScope = prevScope;
            return currentScope;
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


    Dictionary<Type, IBox> registrations = [];

    public IBox GetInstanceBox<I>()
    {
        return GetInstance(typeof(I));
    }

    public I GetInstance<I>()
    {
        return (I)GetInstance(typeof(I)).GetObject;
    }

    public IBox GetInstance(Type type)
    {
        if (!registrations.ContainsKey(type))
            throw new ArgumentException("Был получен незарегистрированный интерфейс");
        return registrations[type];
    }

    public void Register<I, C>(LifeStyle lifeStyle, params object[] extra) where C : class, I
    {
        switch (lifeStyle)
        {
            case LifeStyle.PerRequest:
                RegisterPerRequest<I, C>(() => Create<I, C>(extra));
                break;
            case LifeStyle.Scoped:
                RegisterScoped<I, C>(() => Create<I, C>(extra));
                break;
            case LifeStyle.Singleton:
                RegisterSingleton<I, C>(() => Create<I, C>(extra));
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

    private C Create<I, C>(params object[] extra) where C : class, I
    {
        var type = typeof(C);
        var constructor = type.GetConstructors().First();
        var parameters = constructor.GetParameters();
        var resolvedParams = parameters.Select(p =>
        {
            if (registrations.ContainsKey(p.ParameterType))
                return GetInstance(p.ParameterType).GetObject;
            return null;
        }).ToArray();

        int j = 0;
        for (int i = 0; i < resolvedParams.Length; i++)
        {
            if (resolvedParams[i] == null)
            {
                resolvedParams[i] = extra[j];
                j++;
            }
        }

        return (C)Activator.CreateInstance(type, resolvedParams);
    }

    private void RegisterPerRequest<I, C>(Func<C> fabric) where C : class, I
    {
        registrations[typeof(I)] = new BoxPerRequest(fabric);
    }

    private void RegisterScoped<I, C>(Func<C> fabric) where C : class, I
    {
        registrations[typeof(I)] = new BoxScope(fabric);
    }

    private void RegisterSingleton<I, C>(Func<C> fabric) where C : class, I
    {
        registrations[typeof(I)] = new BoxSingleton(fabric);
    }

    public DIContainer CreateScope()
    {
        foreach (var i in registrations)
            if (i.Value is BoxScope scope)
                registrations[i.Key] = scope.CreateScope();


        return this;
    }

    public void Dispose()
    {
        foreach (var i in registrations)
            if (i.Value is BoxScope scope)
                registrations[i.Key] = scope.Dispose();
    }
}
