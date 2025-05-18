namespace Lab_5;

public interface IUserRepository : IDataRepository<User>
{
    public User? GetByLogin(string login);
}
