using Abstractions.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Common;
using Persistence.DbContexts;
using Shared.Pagination;

namespace Persistence.Repositories;

public class TagsRepositoryRO : RepositoryROBase<Tag, StackOverFlowDbContextRO>, ITagsRepositoryRO
{
    public TagsRepositoryRO(StackOverFlowDbContextRO dbContexts) : base(dbContexts)
    {
    }

    public async Task<PagedList<Tag>> GetTagsAsync(int page, int pageSize, string sortBy, bool descanding, CancellationToken ct)
    {
        var totalCount = await _dbContext.Tags.CountAsync(ct);


        var tags = descanding != true ? 
            await _dbContext.Tags
            .OrderBy(o => EF.Property<object>(o, sortBy))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct)
            :
             await _dbContext.Tags
            .OrderByDescending(o => EF.Property<object>(o, sortBy))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);


        return new PagedList<Tag>(page, pageSize, totalCount, tags);
    }
}
