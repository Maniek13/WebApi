using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories;

public interface IUserRepository
{
    public Task AddOrUpdateUsersAsync(List<User> questions, CancellationToken ct);
}
