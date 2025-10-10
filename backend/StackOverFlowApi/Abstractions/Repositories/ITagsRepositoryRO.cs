using Domain.Entities;

namespace Abstractions.Repositories;

public interface ITagsRepositoryRO
{
    public IEnumerable<Tag> GetTags();
}
