using Abstractions.Repositories.SOF;
using Domain.Entities.App;
using Domain.Entities.StackOverFlow;
using Microsoft.EntityFrameworkCore;
using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Adapters;
public class Query
{
    [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<User> GetUsers(IUsersRepositoryRO userRepository, int? minQuestionCount = null) 
    {
        var query = userRepository.GetAll().Include(u => u.Questions).AsQueryable();

        if (minQuestionCount.HasValue)
            query = query.Where(u => u.Questions.Count >= minQuestionCount.Value);

        return query;
    }

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

    [UsePaging(IncludeTotalCount = true, MaxPageSize = 100)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<ApplicationUser> GetAppUsers(Abstractions.Repositories.Api.IUsersRepositoryRO userRepository) => userRepository.GetAll();
}
