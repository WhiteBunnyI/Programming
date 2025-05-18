using System.Text.Json;

namespace Lab_5;

public class AuthService : IAuthService
{
    readonly string path;
    public AuthService(string authFilePath)
    {
        path = authFilePath;
        if (!File.Exists(authFilePath)) CreateFile();
    }

    private void CreateFile() => File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None).Close();

    private User? GetUser()
    {
        try
        {
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return JsonSerializer.Deserialize<User>(stream);
        }
        catch
        {
            return null;
        }
    }

    public void SignIn(User user)
    {
        SignOut();
        using var stream = File.Open(path, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
        JsonSerializer.Serialize(stream, user);
        stream.Flush();
    }

    public void SignOut()
    {
        CreateFile();
    }
    public bool IsAuthorized => GetUser() != null;

    public User? User => GetUser();
}
