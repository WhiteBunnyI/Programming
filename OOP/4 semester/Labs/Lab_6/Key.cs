namespace Lab_6;

public struct Key
{
    public char? key;
    public ConsoleModifiers modifiers;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key">Передайте null для того, чтобы захватывать любые кнопки</param>
    /// <param name="modifiers"></param>
    public Key(char? key, ConsoleModifiers modifiers)
    {
        this.key = key;
        this.modifiers = modifiers;
    }

    public override string ToString()
    {
        if (modifiers != ConsoleModifiers.None)
            return $"{modifiers.ToString()}+{key}";
        return $"{key}";
    }
}
