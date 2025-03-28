using System.IO;

namespace Practice_5
{
    public class TXTDataBase : IUserRepository
    {
        private FileStream database;
        public TXTDataBase(string filename)
        {
            database = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }

        public void Create(UserData data)
        {
            
        }

        public UserData Read()
        {
            throw new NotImplementedException();
        }

        public void Update(UserData data)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void GetUser(string login)
        {
            throw new NotImplementedException();
        }

        public List<UserData> GetUsers()
        {
            throw new NotImplementedException();
        }

    }
}
