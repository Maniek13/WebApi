using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories.SOF;

public interface IUsersRepositoryRO : IRepositoryROBase<User>
{
    public bool CheckIfUserExistByUserId(long userId);
}
