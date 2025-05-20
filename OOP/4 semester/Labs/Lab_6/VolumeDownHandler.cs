namespace Lab_6;

internal class VolumeDownHandler : IKeyHandler
{
    public void Execute(Key key)
    {
        VirtualKeyboard.LogToFile("Volume decrease 20%");
    }

    public void Undo(Key key)
    {
        VirtualKeyboard.LogToFile("Volume increase 20%");
    }
}
