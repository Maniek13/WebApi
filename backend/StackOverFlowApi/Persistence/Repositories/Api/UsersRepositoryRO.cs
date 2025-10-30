using Abstractions.Repositories.Api;
using Domain.Entities.App;
using Persistence.Common;
using Persistence.DbContexts.App;

namespace Persistence.Repositories.Api;

public class UsersRepositoryRO : RepositoryROBase<ApplicationUser, AppDbContext>, IUsersRepositoryRO
{
    public UsersRepositoryRO(AppDbContext dbContexts) : base(dbContexts)
    {
    }
}
