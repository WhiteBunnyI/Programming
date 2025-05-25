namespace Lab_7;

public interface ILogger
{
    public void Log(string message);
}

public class ConsoleLogger : ILogger
{
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
        using var stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
        using var writer = new StreamWriter(stream);
        writer.Write(message);
    }
}