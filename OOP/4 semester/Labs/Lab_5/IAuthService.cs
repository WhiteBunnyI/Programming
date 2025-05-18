namespace Lab_5;

public interface IAuthService
{
    public void SignIn(User user);
    public void SignOut();
    public bool IsAuthorized { get; }
    public User? User { get; }

}
