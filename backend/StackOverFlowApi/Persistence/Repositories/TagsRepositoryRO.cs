using Abstractions.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class TagsRepositoryRO : ITagsRepositoryRO
{
    public async Task<IEnumerable<Tag>> GetTags(int page, int PageSize, string SortBy, bool descanding, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
