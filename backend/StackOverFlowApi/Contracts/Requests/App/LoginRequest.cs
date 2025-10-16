namespace Contracts.Requests.App;

public class LoginRequest
{
    public string Name { get; set; } = default!;
    public string Password { get; set; } = default!;
}
