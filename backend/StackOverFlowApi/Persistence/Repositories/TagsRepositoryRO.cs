using Abstractions.Interfaces;
using Common.Interfaces;
using Domain.Entities;
using Persistence.DbContexts;

namespace Persistence.Repositories;

public class TagsRepositoryRO : RepositoryROBase<Tag, StackOverFlowDbContextRO>, ITagsRepositoryRO
{
    public TagsRepositoryRO(StackOverFlowDbContextRO dbContexts) : base(dbContexts)
    {
    }

    public async Task<IEnumerable<Tag>> GetTags(int page, int PageSize, string SortBy, bool descanding, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
