using Application.Interfaces.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task CreateUserAsync(string login, string password, string role = "User")
    {
        var user = new IdentityUser { UserName = login};
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            if (result.Errors.Any())
                throw new Exception(result.Errors.FirstOrDefault()?.Description);
            else
                throw new Exception("Unhandled error");

        if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

        await _userManager.AddToRoleAsync(user, role);
    }
    public async Task<string?> LoginAsync(string login, string password)
    {
        var user = await _userManager.FindByNameAsync(login);

        if (user == null)
            throw new ArgumentException("User does not exist");

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new ArgumentException("Wrong password");

        return await GenerateJwtTokenAsync(user);
    }

    private string GetJwtKey() => _config["Jwt:Key"]!;
    private string GetIssuer() => _config["Jwt:Issuer"]!;
    private string GetAudience() => _config["Jwt:Audience"]!;
    private int GetExpiryInHours() => int.Parse(_config["Jwt:ExpiryInHours"] ?? "1");

    private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email?? ""),
        };

        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);

        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtKey()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: GetIssuer(),
            audience: GetAudience(),
            claims: claims,
            expires: DateTime.UtcNow.AddHours(GetExpiryInHours()),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
