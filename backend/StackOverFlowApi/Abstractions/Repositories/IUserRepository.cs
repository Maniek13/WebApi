using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories;

public interface IUserRepository
{
    bool CheckIfUserExistByUserId(long userId);
    Task AddOrUpdateUsersAsync(List<User> questions, CancellationToken ct);
}
