namespace Lab_5;

public class UserRepository : DataRepository<User>
{
    public UserRepository(string filePath) : base(filePath) { }
}
