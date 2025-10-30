using Domain.Entities.App.ValueObjects;

namespace Abstractions.Repositories.Api;

public interface IUserRepository
{
    Task SetUserAddress(string userId, UserAddress address, CancellationToken ct);
}
