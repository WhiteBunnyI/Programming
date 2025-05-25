using System.Text.Json;

namespace Lab_5;

public class DataRepository<T> : IDataRepository<T> where T : IIdentifiable
{
    protected string filePath;
    protected JsonSerializerOptions options = new() { WriteIndented = true, IncludeFields = true };

    public DataRepository(string filePath)
    {
        this.filePath = filePath;
        InitFile();
    }

    protected virtual void InitFile()
    {
        using var stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        var reader = new StreamReader(stream);
        if(reader.Read() == -1)
            JsonSerializer.Serialize(stream, new List<T>(), options);
        stream.Flush();
    }

    protected virtual void SaveToFile(List<T> items)
    {
        using var writer = new StreamWriter(filePath);
        var serialize = JsonSerializer.Serialize(items, options);
        writer.Write(serialize);
        writer.Flush();
    }

    protected virtual List<T> ReadFromFile()
    {
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        List<T> result = JsonSerializer.Deserialize<List<T>>(stream, options) ?? [];
        return result;
    }

    public virtual void Add(T item)
    {
        var items = ReadFromFile();
        if(items.FindIndex(x => x.Id == item.Id) == -1)
        {
            items.Add(item);
            SaveToFile(items);
        }
    }
    public virtual List<T> GetAll()
    {
        return ReadFromFile();
    }

    public virtual T? GetById(int id)
    {
        return ReadFromFile().Find(x => x.Id == id);
    }

    public virtual void Update(T item)
    {
        var items = ReadFromFile();
        int index = items.FindIndex(x => x.Id == item.Id);
        items.RemoveAt(index);
        items.Insert(index, item);
        SaveToFile(items);
    }

    public virtual void Delete(T item)
    {
        var items = ReadFromFile();
        items.Remove(item);
        SaveToFile(items);
    }


}
