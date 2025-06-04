using Lab_7;

DIContainer container = new DIContainer();

container.Register<ILogger, ConsoleLogger>(LifeStyle.Scoped);
container.Register<IService, PCService>(LifeStyle.Singleton);
container.Register<ICustomer, OneCustomer>(LifeStyle.PerRequest, "Oleg");

ICustomer oneCustomer;
ICustomer otherCustomer;

using (var container1 = container.CreateScope())
{
    oneCustomer = container1.GetInstance<ICustomer>();
    oneCustomer.RequestWork();
    var logger = container1.GetInstance<ILogger>();

    //container1.Register<ILogger, FileLogger>(LifeStyle.Scoped, "../../../log.txt");

    using (var container2 = container1.CreateScope())
    {
        oneCustomer = container2.GetInstance<ICustomer>();
        otherCustomer = container2.GetInstance<ICustomer>();

        var logger1 = container2.GetInstance<ILogger>();
        var logger2 = container2.GetInstance<ILogger>();

        Console.WriteLine($"Is customers equal: {oneCustomer == otherCustomer}");
        Console.WriteLine($"Is loggers equal: {logger1 == logger2}");
        Console.WriteLine($"Is loggers in other scope equal: {logger == logger1}");
    }

    var logger3 = container1.GetInstance<ILogger>();
    Console.WriteLine($"Is loggers equal: {logger == logger3}");
}

/*using (var logger = (BoxScope)container.GetInstanceBox<ILogger>())
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
*/