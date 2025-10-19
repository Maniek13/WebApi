using Abstractions.Repositories;
using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts.StackOverFlow;

namespace Persistence.Repositories.StackOverFlow;

public class TagsRepository : ITagsRepository
{
    private readonly StackOverFlowDbContext _dbContext;

    public TagsRepository(StackOverFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SetTagsAsync(List<Tag> tags, CancellationToken ct)
    {
        await _dbContext.Tags.ExecuteDeleteAsync();
        await _dbContext.Tags.AddRangeAsync(tags, ct);
        await _dbContext.SaveChangesAsync();
    }
}
