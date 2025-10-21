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

    public async Task AddOrUpdateUsersAsync(List<User> questions, CancellationToken ct)
    {
        for (int i = 0; i < questions.Count; ++i)
        {
            var question = await _dbContext.Users.FirstOrDefaultAsync(el => el.AccountId.Equals(questions[i].AccountId));

            if (question == null)
                await _dbContext.Users.AddAsync(questions[i]!, ct);
            else
            {
                question.Update(questions[i].DispalaName);
                _dbContext.Users.Update(question);
            }
        }

        await _dbContext.SaveChangesAsync(ct);
    }
}
