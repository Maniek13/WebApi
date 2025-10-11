using Domain.Entities;

namespace Abstractions.Interfaces;

public interface ITagsRepository
{
    public Task SetTags(List<Tag> tags, CancellationToken ct);
}
