using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories;

public interface IUsersRepositoryRO : IRepositoryROBase<User>
{
    public bool CheckIfUserExistByUserId(long userId);
}
