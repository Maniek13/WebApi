namespace Application.Interfaces.App;

public interface IAuthService
{
    public Task CreateUserAsync(string login, string password, string role = "User");
    public Task<(string accesToken, string refreshToken)> LoginAsync(string login, string password, string ipAddress);
    public Task<(string accesToken, string refreshToken)> RefreshToken(string refreshToken, string ipAddress);  
}
