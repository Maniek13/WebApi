using Application.Interfaces.App;
using Domain.Entities.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Exceptions;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    public async Task CreateUserAsync(string login, string password, string[] role)
    {
        var user = new ApplicationUser { UserName = login};
        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            if (result.Errors.Any())
                throw new ValidationExceptions(result.Errors.First().Description);
            else
                throw new Exception("Unhandled error");

        if(role.Length == 0)
        {
            if (!await _roleManager.RoleExistsAsync("User"))
                await _roleManager.CreateAsync(new IdentityRole("User"));

            await _userManager.AddToRoleAsync(user, "User");

            return;
        }


        for(int i = 0; i < role.Length; ++i)
        {
            if (!await _roleManager.RoleExistsAsync(role[i]))
                await _roleManager.CreateAsync(new IdentityRole(role[i]));

            await _userManager.AddToRoleAsync(user, role[i]);
        }
    }

    public async Task<(string accesToken, string refreshToken)> RefreshTokenAsync(string refreshToken, string ipAddress)
    {
        var user = await _userManager.Users
           .Include(u => u.RefreshTokens)
           .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken));

        if (user == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        var token = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

        if (token == null || token.IsExpired)
            throw new UnauthorizedAccessException("Invalid or expired refresh token");

        user.RefreshTokens.Remove(token);
        user.RefreshTokens.RemoveAll(x => x.IsExpired);

        var newRefreshToken = GenerateRefreshToken(ipAddress);

        user.RefreshTokens.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        var newAccessToken = await GenerateJwtTokenAsync(user);

        return (await GenerateJwtTokenAsync(user), newRefreshToken.Token);
    }

    public async Task<(string accesToken, string refreshToken)> LoginAsync(string login, string password, string ipAddress)
    {
        var user = await _userManager.FindByNameAsync(login);

        if (user == null)
            throw new ValidationExceptions("User does not exist");

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new ValidationExceptions("Wrong password");

        var refreshToken = GenerateRefreshToken(ipAddress);

        user.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        return (await GenerateJwtTokenAsync(user), refreshToken.Token);
    }

    private string GetJwtKey() => _config["Jwt:Key"]!;
    private string GetIssuer() => _config["Jwt:Issuer"]!;
    private string GetAudience() => _config["Jwt:Audience"]!;
    private int GetExpiryInHours() => int.Parse(_config["Jwt:ExpiryInHours"] ?? "1");
    private int GetRefreshTokenExpiryInDays() => int.Parse(_config["Jwt:RefreshTokenExpiryInDays"] ?? "1");

    private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
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

    private RefreshToken GenerateRefreshToken(string ipAddress)
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            Expires = DateTime.UtcNow.AddDays(GetRefreshTokenExpiryInDays()),
            CreatedByIp = ipAddress
        };
    }

}
