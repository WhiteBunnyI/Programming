namespace Lab_6;

public partial class VirtualKeyboard
{
    static List<Key> _history;
    static int _historyIndex = 0;

    static Dictionary<Key, List<IKeyHandler>> _handlers;

    static StreamWriter _writer;
    static VirtualKeyboard()
    {
        _history = [];
        _handlers = [];
        var stream = File.Open("../../../Log.txt", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
        _writer = new StreamWriter(stream);
    }

    private static void Recover()
    {
        foreach (Key key in _history)
        {
            Proceed(key, false);
        }

        for (int i = 0; i < _history.Count - _historyIndex; i++)
            LogToConsole("Undo");
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
        if (key.key == (int)ConsoleKey.Escape)
        {
            Undo();
            return;
        }

        if (key.key == (int)ConsoleKey.Tab)
        {
            Redo();
            return;
        }

        List<Key> _keys = [
            key,
            new Key(null, key.modifiers)
        ];

        if (writeInHistory)
        {
            if (_historyIndex != _history.Count)
                _history = _history[.._historyIndex];
            _history.Add(key);
            _historyIndex++;
        }

        LogToConsole(key.ToString());

        foreach (var _key in _keys)
        {
            var handlers = GetHandler(_key);
            if (handlers == null) continue;

            foreach (var handler in handlers)
                handler.Execute(key);
        }
            
    }

    private static List<IKeyHandler>? GetHandler(Key key)
    {
        if (_handlers.TryGetValue(key, out var handlers))
            return handlers;

        return null;
    }
    
    public static void Undo()
    {
        if (_historyIndex == 0) return;
        LogToConsole("Undo");

        _historyIndex--;
        Key key = _history[_historyIndex];

        var handlers = GetHandler(key);
        if (handlers == null) return;
        foreach(var handler in handlers)
            handler.Undo(key);
    }

    public static void Redo()
    {
        if(_historyIndex == _history.Count) return;
        LogToConsole("Redo");

        Key key = _history[_historyIndex];
        _historyIndex++;

        var handlers = GetHandler(key);
        if (handlers == null) return;
        foreach (var handler in handlers)
            handler.Execute(key);
    }

    public static void AddHotKey(IKeyHandler handler, Key key)
    {
        _handlers.TryAdd(key, []);
        _handlers[key].Remove(handler);    //Избегаем дублирования
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
