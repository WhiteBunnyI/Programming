using Lab_6;

VirtualKeyboard.AddHotKey(PrintKeyHandler.Instance, new Key(null, ConsoleModifiers.None));
VirtualKeyboard.AddHotKey(VolumeUpHandler.Instance, new Key('+', ConsoleModifiers.Alt));
VirtualKeyboard.AddHotKey(VolumeDownHandler.Instance, new Key('-', ConsoleModifiers.Alt));


while (true)
{
    var key = Console.ReadKey(true);
    VirtualKeyboard.Proceed(key);
}