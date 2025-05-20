namespace Lab_6;

public class PrintKeyHandler : IKeyHandler
{
    string text = "";

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
