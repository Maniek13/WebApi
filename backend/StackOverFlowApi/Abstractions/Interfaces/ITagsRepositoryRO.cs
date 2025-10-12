using Domain.Entities;
using Shared.Pagination;

namespace Abstractions.Interfaces;

public interface ITagsRepositoryRO : IRepositoryROBase<Tag>
{
    public Task<PagedList<Tag>> GetTags(int page, int pageSize, string sortBy, bool descanding, CancellationToken ct);
}
