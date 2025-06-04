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
    string filePath;
    public FileLogger(string filePath)
    {
        this.filePath = filePath;
        
    }
    public void Log(string message)
    {
        var stream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
        var writer = new StreamWriter(stream);
        writer.Write(message+'\n');
        writer.Flush();
    }
}