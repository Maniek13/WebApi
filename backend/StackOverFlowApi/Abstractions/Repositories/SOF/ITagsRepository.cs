using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories.SOF;

public interface ITagsRepository
{
    public Task SetTagsAsync(List<Tag> tags, CancellationToken ct);
}
