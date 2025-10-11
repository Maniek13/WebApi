using Domain.Entities;

namespace Abstractions.Repositories;

public interface ITagsRepositoryRO
{
    public Task<IEnumerable<Tag>> GetTags(int page, int PageSize, string SortBy, bool descanding, CancellationToken ct);
}
