﻿namespace Lab_7;

public interface IService
{
    public void Work();
}

public class PCService : IService
{
    ILogger logger;
    public PCService(ILogger logger)
    {
        Console.WriteLine("Create a service");
        this.logger = logger;
    }
    public void Work()
    {
        logger.Log("PC service is working...");
    }
}

public class NetService : IService
{
    ILogger logger;
    public NetService(ILogger logger)
    {
        Console.WriteLine("Create a service");
        this.logger = logger;
    }
    public void Work()
    {
        logger.Log("Net service is working...");
    }
}