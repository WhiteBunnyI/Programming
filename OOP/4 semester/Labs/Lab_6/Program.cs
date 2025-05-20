using Lab_6;

string keyboardStatePath = "../../../save.state";

VirtualKeyboard.AddHotKey(new PrintKeyHandler(), new Key(null, ConsoleModifiers.None));
VirtualKeyboard.AddHotKey(new VolumeUpHandler(), new Key('+', ConsoleModifiers.Alt));
VirtualKeyboard.AddHotKey(new VolumeDownHandler(), new Key('-', ConsoleModifiers.Alt));

VirtualKeyboardSaver.Recover(keyboardStatePath);

for(int i = 0; i < 10; i++)
{
    var key = Console.ReadKey(true);
    VirtualKeyboard.Proceed(key);
}

VirtualKeyboardSaver.Save(keyboardStatePath);