namespace Lab_7;

public interface ICustomer
{
    public void RequestWork();
}

public class OneCustomer : ICustomer
{
    ILogger logger;
    IService service;
    string name;

    public OneCustomer(ILogger logger, IService service, string name)
    {
        Console.WriteLine("Create a customer");
        this.logger = logger;
        this.service = service;
        this.name = name;
    }

    public void RequestWork()
    {
        logger.Log($"{name} is coming!");
        service.Work();
    }
}

public class ThreeCustomer : ICustomer
{
    ILogger logger;
    IService service;

    public ThreeCustomer(ILogger logger, IService service)
    {
        this.logger = logger;
        this.service = service;
    }

    public void RequestWork()
    {
        logger.Log("Customer is coming!");
        service.Work();
        service.Work();
        service.Work();
    }
}