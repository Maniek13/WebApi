using Domain.Entities.App.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.App;
public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }

    public ApplicationUser(UserAddress userAddress, List<UserMessage> messages, List<RefreshToken> refreshTokens)
    {
        UserAddress = userAddress;
        Messages = messages;
        RefreshTokens = refreshTokens;
    }

    public UserAddress? UserAddress { get; private set; }
    public List<UserMessage> Messages { get; init; } = [];
    public List<RefreshToken> RefreshTokens { get; set; } = [];

    public void UpdateAddress(UserAddress? userAddress)
    {
        UserAddress = userAddress;
    }
    public void AddMessage(UserMessage message)
    {
        Messages.Add(message);
    }
}
