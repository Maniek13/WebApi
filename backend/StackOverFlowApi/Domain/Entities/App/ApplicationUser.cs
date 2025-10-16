using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.App;
public class ApplicationUser : IdentityUser
{
    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
