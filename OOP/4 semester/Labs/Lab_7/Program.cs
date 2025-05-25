using Lab_7;
using static Lab_7.DIContainer;

DIContainer container = new DIContainer();

container.Register<ILogger, ConsoleLogger>(LifeStyle.Scoped);
container.Register<IService, PCService>(LifeStyle.Singleton);
container.Register<ICustomer, OneCustomer>(LifeStyle.PerRequest, "Oleg");

ICustomer oneCustomer;
using (var logger = (BoxScope)container.GetInstanceBox<ILogger>())
{
    oneCustomer = container.GetInstance<ICustomer>();
    oneCustomer.RequestWork();

    container.Register<ILogger, FileLogger>(LifeStyle.Scoped, "../../../log.txt");
    using (var logger1 = (BoxScope)container.GetInstanceBox<ILogger>())
    {
        oneCustomer = container.GetInstance<ICustomer>();
        oneCustomer.RequestWork();

        container.Register<ICustomer, OneCustomer>(LifeStyle.PerRequest,
            () => new OneCustomer(container.GetInstance<ILogger>(), container.GetInstance<IService>(), "Ivan"));
        container.Register<ILogger, ConsoleLogger>(LifeStyle.Scoped);
        container.Register<IService, NetService>(LifeStyle.PerRequest);
        using (var logger2 = (BoxScope)container.GetInstanceBox<ILogger>())
        {
            oneCustomer = container.GetInstance<ICustomer>();
            oneCustomer.RequestWork();
        }

        oneCustomer = container.GetInstance<ICustomer>();
        oneCustomer.RequestWork();
    }

    oneCustomer = container.GetInstance<ICustomer>();
    oneCustomer.RequestWork();
}
