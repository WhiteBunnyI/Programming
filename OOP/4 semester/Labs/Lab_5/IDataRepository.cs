namespace Lab_5;

public interface IDataRepository<T>
{
    public List<T> GetAll();
    public T? GetById(int id);
    public void Add(T item);
    public void Update(T item);
    public void Delete(T item);
}
