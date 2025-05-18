namespace Lab_3;

internal class SocketHandler : ILogHandler
{
    public void Handle(string message)
    {
        Console.WriteLine($"Emulate socket: {message}");
    }
}
