using Domain.Entities.StackOverFlow;
using Shared.Pagination;

namespace Abstractions.Interfaces;

public interface ITagsRepositoryRO : IRepositoryROBase<Tag>
{
    public Task<PagedList<Tag>> GetTagsAsync(int page, int pageSize, string sortBy, bool descanding, CancellationToken ct);
}
