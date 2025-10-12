using Domain.Entities;

namespace Abstractions.Interfaces;

public interface ITagsRepository
{
    public Task SetTagsAsync(List<Tag> tags, CancellationToken ct);
}
