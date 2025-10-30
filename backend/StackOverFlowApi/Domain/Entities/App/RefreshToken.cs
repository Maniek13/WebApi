using Domain.Entities.App.EntityIds;
using Shared.Domain;

namespace Domain.Entities.App;

public class RefreshToken : Entity<RefreshTokenId>
{
    public RefreshToken() : base() { }

    public string Token { get; set; } = Guid.NewGuid().ToString();
    public DateTime Expires { get; set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string? CreatedByIp { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser? User { get; set; }
}
