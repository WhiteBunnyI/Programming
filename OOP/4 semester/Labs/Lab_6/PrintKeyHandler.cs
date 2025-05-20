namespace Lab_6;

public class PrintKeyHandler : IKeyHandler
{
    public static PrintKeyHandler Instance { get; private set; }
    static string text = "";

    static PrintKeyHandler()
    {
        Instance = new PrintKeyHandler();
    }

    public void Execute(Key key)
    {
        text += key.key;
        VirtualKeyboard.LogToFile(text);
    }

    public void Undo(Key key)
    {
        text = text[..^1];
        VirtualKeyboard.LogToFile(text);
    }
}
