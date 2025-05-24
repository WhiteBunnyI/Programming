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
        }

        static JsonSerializerOptions _options;

        static VirtualKeyboardSaver()
        {
            _options = new() { WriteIndented = true, IncludeFields = true, ReferenceHandler = ReferenceHandler.Preserve };
            _options.AddAutoDerivedTypes();
        }

        public static void Recover(string filePath)
        {
            using var stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
            using var reader = new StreamReader(stream);
            string text = reader.ReadToEnd();
            if (text.Length == 0)
                return;

            SaveState save = JsonSerializer.Deserialize<SaveState>(text, _options) ?? new();
            _globalHistory = save.history ?? [];
            foreach (var pair in _handlers)
            {
                foreach (var handler in pair.Value)
                    AddHotKey(handler, pair.Key);
            }
            VirtualKeyboard.Recover();
        }

        public static void Save(string filePath)
        {
            using var stream = File.OpenWrite(filePath);
            SaveState save = new SaveState() { history = _globalHistory, handlers = _handlers.ToList() };
            JsonSerializer.Serialize(stream, save, _options);

            stream.Flush();
        }
    }
}
