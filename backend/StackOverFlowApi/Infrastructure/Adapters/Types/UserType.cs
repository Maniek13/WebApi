using Domain.Entities.StackOverFlow;

namespace Infrastructure.Adapters.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Ignore(u => u.CreatedAt);

        descriptor.Field(u => u.UserNumber).Name("userId").Resolve(ctx => ctx.Parent<User>().UserNumber.Value).Type<NonNullType<LongType>>();
        descriptor.Field(u => u.DisplayName).Type<StringType>();
        descriptor.Field(u => u.CreatedDate).Type<DateTimeType>();
    }
}
