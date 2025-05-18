namespace Lab_3;

internal class FileHandler : ILogHandler
{
    StreamWriter m_writer;
    public FileHandler(string path)
    {
        var m_stream = File.Open($"{path}/Log.txt", FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        m_writer = new StreamWriter(m_stream);
    }

    public FileHandler() : this("../../../") { }
    

    public void Handle(string message)
    {
        m_writer.WriteLine(message);
        m_writer.Flush();
    }
}
