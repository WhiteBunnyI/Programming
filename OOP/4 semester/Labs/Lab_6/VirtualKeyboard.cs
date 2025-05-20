namespace Lab_6;

public class VirtualKeyboard
{
    static List<(IKeyHandler hanlder, Key key)> _history;
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

    public static void Proceed(ConsoleKeyInfo key)
    {
        if(key.Key == ConsoleKey.Escape)
        {
            Undo();
            return;
        }

        List<Key> _keys = [
            new Key(key.KeyChar, key.Modifiers), 
            new Key(null, key.Modifiers)
        ];

        LogToConsole(_keys[0].ToString());

        foreach(var _key in _keys)
        {
            if (_handlers.TryGetValue(_key, out var handlers))
            {
                foreach (IKeyHandler handler in handlers)
                {
                    handler.Execute(_keys[0]);
                    if(_historyIndex != _history.Count)
                        _history = _history[.._historyIndex];
                    _history.Add(new (handler, _keys[0]));
                    _historyIndex++;
                }
            }
        }
    }

    public static void Undo()
    {
        if (_historyIndex == 0) return;
        _historyIndex--;
        (IKeyHandler handler, Key key) = _history[_historyIndex];
        handler.Undo(key);
        LogToConsole("Undo");
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
