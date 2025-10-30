using Abstractions.Repositories;
using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Persistence.Common;
using Persistence.DbContexts.StackOverFlow;
using Shared.Pagination;
using System.Linq.Expressions;

namespace Persistence.Repositories.StackOverFlow;

public class TagsRepositoryRO : RepositoryROBase<Tag, StackOverFlowDbContextRO>, ITagsRepositoryRO
{
    public TagsRepositoryRO(StackOverFlowDbContextRO dbContexts) : base(dbContexts)
    {
    }

    public async Task<PagedList<Tag>> GetTagsAsync(int page, int pageSize, string sortBy, bool descanding, CancellationToken ct)
    {
        var totalCount = await _dbContext.Tags.CountAsync(ct);


        var tag1 = await _dbContext.Tags
            .OrderByDynamic(sortBy, descanding)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return new PagedList<Tag>(page, pageSize, totalCount, tag1);
    }
}
