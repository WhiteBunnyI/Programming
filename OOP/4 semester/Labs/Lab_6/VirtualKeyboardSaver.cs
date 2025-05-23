global using VirtualKeyboardSaver = Lab_6.VirtualKeyboard.VirtualKeyboardSaver;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lab_6;

public partial class VirtualKeyboard
{
    public static class VirtualKeyboardSaver
    {
        private class SaveState()
        {
            public List<Key>? history = null;
            public List<KeyValuePair<Key, List<IKeyHandler>>>? handlers = null;
            public int historyIndex = -1;
        }

        static JsonSerializerOptions _options = new() { WriteIndented = true, IncludeFields = true, ReferenceHandler = ReferenceHandler.Preserve };
        public static void Recover(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            var reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Length == 0)
                return;

            SaveState save = JsonSerializer.Deserialize<SaveState>(text, _options) ?? new();
            _history = save.history ?? [];
            _historyIndex = save.historyIndex;
            _handlers = save.handlers?.ToDictionary() ?? [];
            VirtualKeyboard.Recover();
        }

        public static void Save(string filePath)
        {
            using var stream = File.OpenWrite(filePath);
            SaveState save = new SaveState() { history = _history, handlers = _handlers.ToList(), historyIndex = _historyIndex };
            JsonSerializer.Serialize(stream, save, _options);

            stream.Flush();
        }
    }
}
