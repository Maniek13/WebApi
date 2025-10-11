using Abstractions.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class TagsRepository : ITagsRepository
{
    private readonly StackOverFlowDbContext _dbContext;

    public TagsRepository(StackOverFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SetTags(List<Tag> tags, CancellationToken ct)
    {
        await _dbContext.tags.ExecuteDeleteAsync();
        await _dbContext.tags.AddRangeAsync(tags, ct);
        await _dbContext.SaveChangesAsync();
    }
}
