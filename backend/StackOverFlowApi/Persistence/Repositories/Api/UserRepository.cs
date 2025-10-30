using Abstractions.Repositories.Api;
using Domain.Entities.App.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts.App;

namespace Persistence.Repositories.Api;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task SetUserAddress(string userId, UserAddress address, CancellationToken ct)
    {
        if (!(await _dbContext.Users.AnyAsync(el => el.Id == userId, ct)))
            throw new ArgumentOutOfRangeException("User doesn't exist");

        var user = await _dbContext.Users.FirstAsync(el => el.Id == userId, ct);

        user.UpdateAddress(address);
    }

    public async Task AddMessage(string userId, UserMessage message, CancellationToken ct)
    {
        if (!(await _dbContext.Users.AnyAsync(el => el.Id == userId, ct)))
            throw new ArgumentOutOfRangeException("User doesn't exist");

        var user = await _dbContext.Users.FirstAsync(el => el.Id == userId, ct);

        user.AddMessage(message);
    }
}