using Lab_7;
using static Lab_7.DIContainer;

DIContainer container = new DIContainer();

container.Register<ILogger, ConsoleLogger>(LifeStyle.Scoped);
container.Register<IService, PCService>(LifeStyle.Singleton);
container.Register<ICustomer, OneCustomer>(LifeStyle.PerRequest);

using (var logger = (BoxScope)container.GetInstance<ILogger>())
{
    var oneCustomer = container.GetInstance<ICustomer>();

}
