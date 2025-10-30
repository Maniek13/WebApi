using Domain.Entities.App.ValueObjects;

namespace Infrastructure.Adapters.Types;

public class UserAddressType : ObjectType<UserAddress>
{
    protected override void Configure(IObjectTypeDescriptor<UserAddress> descriptor)
    {
        descriptor.Field(u => u.City).Type<StringType>(); 
        descriptor.Field(u => u.Street).Type<StringType>();
        descriptor.Field(u => u.ZipCode).Type<StringType>();
    }
}
