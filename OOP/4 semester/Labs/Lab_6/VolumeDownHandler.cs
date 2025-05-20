namespace Lab_6;

internal class VolumeDownHandler : IKeyHandler
{
    public static VolumeDownHandler Instance { get; private set; }

    static VolumeDownHandler()
    {
        Instance = new VolumeDownHandler();
    }

    public void Execute(Key key)
    {
        VirtualKeyboard.LogToFile("Volume decrease 20%");
    }

    public void Undo(Key key)
    {
        VirtualKeyboard.LogToFile("Volume increase 20%");
    }
}
