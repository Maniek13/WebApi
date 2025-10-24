using Abstractions.Repositories;
using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.Repositories.StackOverFlow;

public class UserRepository : IUserRepository
{
    private readonly StackOverFlowDbContext _dbContext;

    public UserRepository(StackOverFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddOrUpdateUsersAsync(List<User> users, CancellationToken ct)
    {
        for (int i = 0; i < users.Count; ++i)
        {
            var question = await _dbContext.Users.FirstOrDefaultAsync(el => el.UserId.Equals(users[i].UserId));

            if (question == null)
                await _dbContext.Users.AddAsync(users[i]!, ct);
            else
            {
                question.Update(users[i].DisplayName);
            }
        }

        await _dbContext.SaveChangesAsync(ct);
    }

    public bool CheckUserExist(long userId) =>
        _dbContext.Users.FirstOrDefault(el => el.UserId == userId) == null ? false : true;


}
