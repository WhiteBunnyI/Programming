namespace Lab_3;

internal class ConsoleHandler : ILogHandler
{
    public void Handle(string message)
    {
        Console.WriteLine(message);
    }
}
