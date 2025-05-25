using System.Security.Cryptography;
using System.Text.Json;

namespace Lab_5;

public class UserRepository : DataRepository<User>
{
    private record class UserSave
    {
        public User user { get; init; }
        public byte[] iv { get; init; }
        public string encryptPassword { get; init; }
        public UserSave(User user, byte[] iv, string encryptPassword)
        {
            this.user = user;
            this.iv = iv;
            this.encryptPassword = encryptPassword;
        }
    }

    string keyPath;

    public UserRepository(string filePath, string keyPath) : base(filePath)
    {
        this.keyPath = keyPath;
    }

    protected override void SaveToFile(List<User> items)
    {
        var key = GetEncryptKey();

        List<UserSave> userSaves = new List<UserSave>(items.Count);
        foreach (var item in items)
        {
            (string encryptPassword, byte[] iv) = AesEncryption.Encrypt(item.Password, key);
            UserSave save = new UserSave(item, iv, encryptPassword);
            userSaves.Add(save);
        }

        using var writer = new StreamWriter(filePath);
        var serialize = JsonSerializer.Serialize(userSaves, options);
        writer.Write(serialize);
        writer.Flush();
    }

    protected override List<User> ReadFromFile()
    {
        using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        List<UserSave> result = JsonSerializer.Deserialize<List<UserSave>>(stream, options) ?? [];

        var key = GetEncryptKey();

        List<User> users = new List<User>(result.Count);

        foreach (var item in result)
        {
            User user = item.user;
            string password = AesEncryption.Decrypt(item.encryptPassword, key, item.iv);

            User newUser = new User(user.Id, user.Name, user.Login, password, user.Email, user.Address);
            users.Add(newUser);
        }

        return users;
    }

    private byte[] GetEncryptKey()
    {
        using var stream = File.Open(keyPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        using var byteStream = new StreamReader(stream);
        string stringKey = byteStream.ReadToEnd();
        byte[] key = new byte[stringKey.Length];

        if (stringKey.Length > 0)
        {
            for (int i = 0; i < key.Length; i++)
                key[i] = (byte)stringKey[i];
            return key;
        }
        else
        {
            using Aes aesAlg = Aes.Create();
            key = aesAlg.Key;
            stringKey = "";
            for (int i = 0; i < key.Length; i++)
                stringKey += (char)key[i];
            using var writer = new StreamWriter(stream);
            writer.Write(stringKey);
            return key;
        }
    }
}
