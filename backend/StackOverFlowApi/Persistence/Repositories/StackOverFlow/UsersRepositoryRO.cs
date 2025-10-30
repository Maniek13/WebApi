using Abstractions.Repositories;
using Domain.Entities.StackOverFlow;
using Persistence.Common;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.Repositories.StackOverFlow;

public class UsersRepositoryRO : RepositoryROBase<User, StackOverFlowDbContextRO>, IUsersRepositoryRO
{
    public UsersRepositoryRO(StackOverFlowDbContextRO dbContexts) : base(dbContexts)
    {
    }
    public bool CheckIfUserExistByUserId(long userId) =>
        _dbContext.Users.FirstOrDefault(u => u.UserNumber == userId) == null ? false : true;
}
