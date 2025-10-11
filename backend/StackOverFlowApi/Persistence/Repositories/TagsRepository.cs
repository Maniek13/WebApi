using Abstractions.Repositories;

namespace Persistence.Repositories;

public class TagsRepository : ITagsRepository
{
    public Task RefreshTags(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task SetTags(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
