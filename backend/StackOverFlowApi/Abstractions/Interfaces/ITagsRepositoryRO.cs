using Common.Interfaces;
using Domain.Entities;

namespace Abstractions.Interfaces;

public interface ITagsRepositoryRO : IRepositoryROBase<Tag>
{
    public Task<IEnumerable<Tag>> GetTags(int page, int PageSize, string SortBy, bool descanding, CancellationToken ct);
}
