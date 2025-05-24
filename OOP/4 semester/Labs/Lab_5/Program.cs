using Lab_5;

User[] users =
[
    new User(0, "Oleg", "OlegLogin", "OlegPassword"),
    new User(1, "Roman", "RomanLogin", "RomanPassword"),
    new User(2, "Ivan", "IvanLogin", "IvanPassword", "IvanEmail@mail.ru"),
    new User(3, "Denis", "DenisLogin", "DenisPassword", Address: "DenisAddress, 5"),
];

UserRepository repository = new("../../../User_DB.txt", "../../../key.txt");

foreach(User user in users)
    repository.Add(user);

User changedUser = users[2] with { Address = "IvanAddress, 10" };
repository.Update(changedUser);

foreach(User user in repository.GetAll())
{
    Console.WriteLine(user);
}


Console.WriteLine("\nTest auth service: \n");

AuthService auth = new("../../../Auth.txt");

Console.WriteLine($"Is already auth? {auth.IsAuthorized}");
if (auth.IsAuthorized)
{
    Console.WriteLine($"Current user: {auth.User}");
    Console.WriteLine($"Trying to sign out...");
    auth.SignOut();
    Console.WriteLine($"Is auth? {auth.IsAuthorized}");
}

User signInUser = users[0];
Console.WriteLine($"Trying to sign in as {signInUser.Login}...");
auth.SignIn(signInUser);
Console.WriteLine($"Authorized user: {auth.User}");

signInUser = users[2];
Console.WriteLine($"Trying to sign in as {signInUser.Login}...");
auth.SignIn(signInUser);
Console.WriteLine($"Authorized user: {auth.User}");





