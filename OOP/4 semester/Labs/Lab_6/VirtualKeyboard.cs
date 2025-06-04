namespace Lab_6;

public partial class VirtualKeyboard
{
    public static string logPath = "../../../Log.txt";

    static List<Key> _globalHistory;

    static List<Key> _buffer;
    static int _bufferIndex = 0;

    static Dictionary<Key, List<IKeyHandler>> _handlers;

    static StreamWriter _writer;

    static Dictionary<ConsoleKey, Action> _specialKeys = new()
    {
        { ConsoleKey.Escape,    Undo },
        { ConsoleKey.Tab,       Redo },
    };

    static VirtualKeyboard()
    {
        _globalHistory = [];
        _buffer = [];
        _handlers = [];
        var stream = File.Open(logPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
        _writer = new StreamWriter(stream);
    }

    private static void Recover()
    {
        foreach (Key key in _globalHistory)
            Proceed(key, false);
    }

    public static void Proceed(ConsoleKeyInfo key)
    {
        Proceed(new Key(key.KeyChar, key.Modifiers), true);
    }

    public static void Proceed(Key key)
    {
        Proceed(key, true);
    }

    private static void Proceed(Key key, bool writeInHistory)
    {
        if (writeInHistory)
            _globalHistory.Add(key);

        if (key.key != null && _specialKeys.TryGetValue((ConsoleKey)key.key, out var action))
        {
            action();
            return;
        }

        LogToConsole(key.ToString());

        if (_bufferIndex != _buffer.Count)
            _buffer = _buffer[.._bufferIndex];
        _buffer.Add(key);
        _bufferIndex++;

        var handlers = GetHandler(key);
        foreach (var handler in handlers)
            handler.Execute(key);
    }

    private static List<IKeyHandler> GetHandler(Key key)
    {
        List<Key> _keys = [
            key,
            new Key(null, key.modifiers)
        ];

        List<IKeyHandler> handlers = [];

        foreach (var _key in _keys)
        {
            if (_handlers.TryGetValue(_key, out var found))
                handlers.AddRange(found);
        }

        return handlers;
    }

    public static void Undo()
    {
        if (_bufferIndex == 0) return;
        LogToConsole("Undo");

        _bufferIndex--;
        Key key = _buffer[_bufferIndex];

        var handlers = GetHandler(key);
        foreach (var handler in handlers)
            handler.Undo(key);
    }

    public static void Redo()
    {
        if (_bufferIndex == _buffer.Count) return;
        LogToConsole("Redo");

        Key key = _buffer[_bufferIndex];
        _bufferIndex++;

        var handlers = GetHandler(key);
        foreach (var handler in handlers)
            handler.Execute(key);
    }

    public static void AddHotKey(IKeyHandler handler, Key key)
    {
        _handlers.TryAdd(key, []);

        foreach (var h in _handlers[key])
            if (handler.GetType() == h.GetType()) return;   //Избегаем дублирования

        _handlers[key].Add(handler);
    }

    public static void LogToFile(string message)
    {
        _writer.WriteLine(message);
        _writer.Flush();
    }

    public static void LogToConsole(string message) => Console.WriteLine(message);
    public static void LogToConsole(char message) => Console.WriteLine(message);
}
