using Domain.Dtos;
using Mapster;
using Contracts.Messages;

namespace Application.Mapping.StackOverFlow;

public class UsersMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<UserDto, User>()
           .MapWith(el => new User { AccountId = el.AccountId, DisplayName = el.DispalaName, CreationDate = el.CreatedAt }); 

    }
}
