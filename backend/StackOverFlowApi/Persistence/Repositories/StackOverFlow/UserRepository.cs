using Abstractions.Repositories.SOF;
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
    public bool CheckIfUserExistByUserId(long userId) =>
        _dbContext.Users.FirstOrDefault(u => u.UserNumber == userId) == null ? false : true;
    public async Task AddOrUpdateUsersAsync(List<User> questions, CancellationToken ct)
    {
        for (int i = 0; i < questions.Count; ++i)
        {
            var question = await _dbContext.Users.FirstOrDefaultAsync(el => el.UserNumber.Equals(questions[i].UserNumber));

            if (question == null)
                await _dbContext.Users.AddAsync(questions[i]!, ct);
            else
            {
                question.Update(questions[i].DisplayName);
            }
        }
    }
}
