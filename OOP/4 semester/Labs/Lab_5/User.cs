namespace Lab_5;

public record User : IIdentifiable
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Login { get; init; }
    public string Password { get; init; }
    public string? Email { get; init; }
    public string? Address { get; init; }

    public User(
        int Id, 
        string Name, 
        string Login, 
        string Password, 
        string? Email = null, 
        string? Address = null)
    {
        this.Id = Id;
        this.Name = Name;
        this.Login = Login;
        this.Password = Password;
        this.Email = Email;
        this.Address = Address;
    }

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Login: {Login}, Email: {Email ?? "null"}, Address: {Address ?? "null"}";
    }
}
