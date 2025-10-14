using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories;

public interface ITagsRepository
{
    public Task SetTagsAsync(List<Tag> tags, CancellationToken ct);
}
