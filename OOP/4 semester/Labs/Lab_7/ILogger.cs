namespace Lab_7;

public interface ILogger
{
    public void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public ConsoleLogger()
    {
        Console.WriteLine("Create a logger");
    }
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public class FileLogger : ILogger
{
    StreamWriter writer;
    public FileLogger(string filePath)
    {
        var stream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
        writer = new StreamWriter(stream);
    }

    ~FileLogger()
    {
        writer.Dispose();
    }

    public void Log(string message)
    {
        writer.Write(message+'\n');
        writer.Flush();
    }
}