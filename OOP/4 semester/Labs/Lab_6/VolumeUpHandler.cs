namespace Lab_6;

internal class VolumeUpHandler : IKeyHandler
{
    public static VolumeUpHandler Instance { get; private set; }

    static VolumeUpHandler()
    {
        Instance = new VolumeUpHandler();
    }

    public void Execute(Key key)
    {
        VirtualKeyboard.LogToFile("Volume increase 20%");
    }

    public void Undo(Key key)
    {
        VirtualKeyboard.LogToFile("Volume decrease 20%");
    }
}
