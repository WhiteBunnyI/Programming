namespace Lab_6;

public class MediaPlayerHandler : IKeyHandler
{
    public static MediaPlayerHandler Instance { get; private set; }

    static bool isOpen;

    static MediaPlayerHandler()
    {
        Instance = new MediaPlayerHandler();
    }

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
