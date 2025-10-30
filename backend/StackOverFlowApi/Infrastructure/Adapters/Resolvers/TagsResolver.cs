using Abstractions.Repositories.SOF;
using Domain.Entities.StackOverFlow;
using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Adapters.Resolvers;

public class TagsResolver
{
    public IQueryable<Tag> GetTags(
         [Parent] Question question,
         [Service] ITagsRepositoryRO tagsRepository)
    {
        var tagNames = question.Tags ?? Array.Empty<string>();

        return tagsRepository
            .GetAll()
            .Where(t => tagNames.Contains(t.Name));
    }
}
