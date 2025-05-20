using System.Text.Json.Serialization;

namespace Lab_6;

public interface IKeyHandler
{
    public void Execute(Key key);
    public void Undo(Key key);
}
