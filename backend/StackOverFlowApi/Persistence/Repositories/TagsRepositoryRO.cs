using Abstractions.Repositories;
using Domain.Entities;

namespace Persistence.Repositories;

public class TagsRepositoryRO : ITagsRepositoryRO
{
    public IEnumerable<Tag> GetTags() => throw new NotImplementedException();
}
