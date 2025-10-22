using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Adapters.Types;

public class QuestionType : ObjectType<Question>
{
    protected override void Configure(IObjectTypeDescriptor<Question> descriptor)
    {
        descriptor.Ignore(u => u.CreateDateTimeStamp);
        descriptor.Field(u => u.UserId).Type<LongType>();
        descriptor.Field(u => u.Title).Type<StringType>();
        descriptor.Field(u => u.Tags).Type<ListType<StringType>>();
        descriptor.Field(u => u.Link).Type<StringType>();
        descriptor.Field(u => u.CreatedDate).Type<DateTimeType>();
        descriptor.Field(u => u.Tags)
            .ResolveWith<UserResolvers>(r => r.GetQuestions(default!, default!))
            .UsePaging()
            .UseFiltering()
            .UseSorting()
            .Type<ListType<ObjectType<QuestionDto>>>();
    }
}

public class UserResolvers
{
    public IQueryable<Question> GetQuestions(
        [Parent] Tag tag,         
        [Service] IQuestionsRepositoryRO questionRepository)
    {
        return questionRepository.GetAll()
            .Where(q => q.Tags.Contains(tag.Name));
    }
}
