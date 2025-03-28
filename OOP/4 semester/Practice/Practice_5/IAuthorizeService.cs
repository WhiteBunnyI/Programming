namespace Practice_5
{
    public interface IAuthorizeService
    {
        public void SignIn(string name, string password);
        public void SignOut();
    }
}
