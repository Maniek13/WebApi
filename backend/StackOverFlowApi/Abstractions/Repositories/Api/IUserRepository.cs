using Domain.Entities.App.ValueObjects;

namespace Abstractions.Repositories.Api;

public interface IUserRepository
{
    Task AddMessage(string userId, UserMessage message, CancellationToken ct);
    Task SetUserAddress(string userId, UserAddress address, CancellationToken ct);
}
