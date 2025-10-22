using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Adapters.Types;

public class TagType : ObjectType<Tag>
{
    protected override void Configure(IObjectTypeDescriptor<Tag> descriptor)
    {
        descriptor.Field(u => u.Name).Type<NonNullType<LongType>>();
        descriptor.Field(u => u.Count).Type<StringType>();
        descriptor.Field(u => u.Participation).Type<DateTimeType>();
    }
}
