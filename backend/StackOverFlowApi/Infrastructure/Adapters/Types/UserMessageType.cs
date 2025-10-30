using Domain.Entities.App.ValueObjects;

namespace Infrastructure.Adapters.Types;

public class UserMessageType : ObjectType<UserMessage>
{
    protected override void Configure(IObjectTypeDescriptor<UserMessage> descriptor)
    {
        descriptor.Field(u => u.Message).Type<StringType>();
    }
}
