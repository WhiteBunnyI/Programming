namespace Lab_6;

public class MediaPlayerHandler : IKeyHandler
{
    bool isOpen;

    public void Execute(Key key)
    {
        if (isOpen) return;
        VirtualKeyboard.LogToFile("Media player launched");
        isOpen = true;
    }

    public void Undo(Key key)
    {
        if (!isOpen) return;
        VirtualKeyboard.LogToFile("Media player closed");
        isOpen = false;
    }
}
