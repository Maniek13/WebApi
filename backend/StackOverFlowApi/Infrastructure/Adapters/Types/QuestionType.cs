using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Infrastructure.Adapters.Resolvers;
using System.Diagnostics.CodeAnalysis;
using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Adapters.Types;

public class QuestionType : ObjectType<Question>
{
    protected override void Configure(IObjectTypeDescriptor<Question> descriptor)
    {
        descriptor.Ignore(u => u.CreateDateTimeStamp);

        descriptor.Field(u => u.QuestionNumber).Name("questionId").Resolve(ctx => ctx.Parent<Question>().QuestionNumber.Value).Type<NonNullType<LongType>>();
        descriptor.Field(u => u.UserNumber).Name("userId").Resolve(ctx => ctx.Parent<Question>().UserNumber == null ? null : ctx.Parent<Question>().UserNumber.Value).Type<LongType>();
        descriptor.Field(u => u.Title).Type<StringType>();
        descriptor.Field(u => u.Tags).Type<ListType<StringType>>();
        descriptor.Field(u => u.Link).Type<StringType>();
        descriptor.Field(u => u.CreatedDate).Type<DateTimeType>();

        descriptor
           .Field("tagsInfo")
           .Type<ListType<NonNullType<ObjectType<Tag>>>>()
           .ResolveWith<TagsResolver>(r => r.GetTags(default!, default!))
           .UsePaging()
           .UseFiltering();
    }
}

