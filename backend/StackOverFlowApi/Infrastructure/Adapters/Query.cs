using Abstractions.Repositories;
using Domain.Entities.StackOverFlow;
using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Adapters;
public class Query
{
    [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(IUsersRepositoryRO userRepository) => userRepository.GetAll();

    [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Question> GetQuestions(IQuestionsRepositoryRO questionRepository) => questionRepository.GetAll();


    [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Tag> GetTags(ITagsRepositoryRO tagsRepository) => tagsRepository.GetAll();
}
