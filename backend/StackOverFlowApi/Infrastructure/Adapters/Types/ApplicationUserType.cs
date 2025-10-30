using Domain.Entities.App;

namespace Infrastructure.Adapters.Types;

public class ApplicationUserType : ObjectType<ApplicationUser>
{
    protected override void Configure(IObjectTypeDescriptor<ApplicationUser> descriptor)
    {
        descriptor.Field(u => u.Messages).Type<ListType<UserMessageType>>();
        descriptor.Field(u => u.UserAddress).Type<UserAddressType>();
    }
}
