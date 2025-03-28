namespace Practice_5
{
    public interface IUserRepository
    {
        public void Create(UserData data);
        public UserData Read();
        public void Update(UserData data);
        public void Delete(int id);

        public List<UserData> GetUsers();
        public void GetUser(int id);
        public void GetUser(string login);
    }
}
