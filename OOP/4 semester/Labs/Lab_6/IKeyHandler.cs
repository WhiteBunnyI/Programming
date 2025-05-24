using System.Text.Json.Serialization;

namespace Lab_6;

[JsonDerivedType(typeof(PrintKeyHandler), "PrintKeyHandler")]
[JsonDerivedType(typeof(VolumeUpHandler), "VolumeUpHandler")]
[JsonDerivedType(typeof(VolumeDownHandler), "VolumeDownHandler")]
[JsonDerivedType(typeof(MediaPlayerHandler), "MediaPlayerHandler")]
public interface IKeyHandler
{
    public void Execute(Key key);
    public void Undo(Key key);
}
