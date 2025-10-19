namespace Contracts.Requests.App;

public class RegisterRequest
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
}
