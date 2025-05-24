namespace Lab_6;

[AutoJsonDerivedTypes]
public interface IKeyHandler
{
    public void Execute(Key key);
    public void Undo(Key key);
}
