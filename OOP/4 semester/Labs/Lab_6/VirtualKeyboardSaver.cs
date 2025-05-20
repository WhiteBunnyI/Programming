global using VirtualKeyboardSaver = Lab_6.VirtualKeyboard.VirtualKeyboardSaver;
using System.Text.Json;

namespace Lab_6;

public partial class VirtualKeyboard
{
    public static class VirtualKeyboardSaver
    {
        public static void Recover(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            var reader = new StreamReader(stream);
            if (reader.Read() != '[')
                return;

            _history = JsonSerializer.Deserialize<List<(IKeyHandler hanlder, Key key)>>(stream) ?? [];
        }

        public static void Save(string filePath)
        {
            using var stream = File.OpenWrite(filePath);

            JsonSerializer.Serialize(stream, _history);

            stream.Flush();
        }
    }
}
