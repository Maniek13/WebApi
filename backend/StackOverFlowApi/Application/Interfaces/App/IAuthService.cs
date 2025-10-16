namespace Application.Interfaces.App;

public interface IAuthService
{
    public Task CreateUserAsync(string login, string password, string role = "User");
    public Task<string?> LoginAsync(string login, string password);

}
