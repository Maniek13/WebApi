using Domain.Entities.StackOverFlow;

namespace Abstarction.Repositories;

public interface IUserRepository
{
    public void Add(User user);

    public Task AddOrUpdate(List<User> users);


}
