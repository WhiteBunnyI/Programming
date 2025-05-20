using Lab_6;

VirtualKeyboard.AddHotKey(new PrintKeyHandler(), new Key(null, ConsoleModifiers.None));
VirtualKeyboard.AddHotKey(new VolumeUpHandler(), new Key('+', ConsoleModifiers.Alt));
VirtualKeyboard.AddHotKey(new VolumeDownHandler(), new Key('-', ConsoleModifiers.Alt));


while (true)
{
    var key = Console.ReadKey(true);
    VirtualKeyboard.Proceed(key);
}