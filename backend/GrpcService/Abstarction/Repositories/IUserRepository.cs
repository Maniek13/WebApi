using Domain.Entities.StackOverFlow;

namespace Abstarction.Repositories;

public interface IUserRepository
{
    public Task AddOrUpdateAsync(User user, CancellationToken cancellationToken = default);

    public Task AddOrUpdateAsync(List<User> users);


}
