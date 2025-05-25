namespace Lab_7;

public interface ICustomer
{
    public void RequestWork();
}

public class OneCustomer : ICustomer
{
    ILogger logger;
    IService service;

    public OneCustomer(ILogger logger, IService service)
    {
        this.logger = logger;
        this.service = service;
    }

    public void RequestWork()
    {
        logger.Log("Customer is coming!");
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