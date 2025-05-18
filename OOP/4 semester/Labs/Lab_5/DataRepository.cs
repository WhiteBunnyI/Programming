using System.Text.Json;

namespace Lab_5;

public class DataRepository<T> : IDataRepository<T> where T : IIdentifiable
{
    string filePath;
    JsonSerializerOptions options = new() { WriteIndented = true };

    public DataRepository(string filePath)
    {
        this.filePath = filePath;
        InitFile();
    }

    private void InitFile()
    {
        using var stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        var reader = new StreamReader(stream);
        if(reader.Read() != '[')
            JsonSerializer.Serialize(stream, new List<T>(), options);
        stream.Flush();
    }

    private void SaveToFile(List<T> items)
    {
        using var writer = new StreamWriter(filePath);
        var serialize = JsonSerializer.Serialize(items, options);
        writer.Write(serialize);
        writer.Flush();
    }

    private List<T> ReadFile()
    {
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        List<T> result = JsonSerializer.Deserialize<List<T>>(stream, options) ?? [];
        return result;
    }

    public void Add(T item)
    {
        var items = ReadFile();
        if(items.FindIndex(x => x.Id == item.Id) == -1)
        {
            items.Add(item);
            SaveToFile(items);
        }
    }
    public List<T> GetAll()
    {
        return ReadFile();
    }

    public T? GetById(int id)
    {
        return ReadFile().Find(x => x.Id == id);
    }

    public void Update(T item)
    {
        var items = ReadFile();
        int index = items.FindIndex(x => x.Id == item.Id);
        items.RemoveAt(index);
        items.Insert(index, item);
        SaveToFile(items);
    }

    public void Delete(T item)
    {
        var items = ReadFile();
        items.Remove(item);
        SaveToFile(items);
    }


}
