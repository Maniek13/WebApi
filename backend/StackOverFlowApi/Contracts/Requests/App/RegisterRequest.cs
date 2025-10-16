namespace Contracts.Requests.App;

public class RegisterRequest
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = "User";
}
