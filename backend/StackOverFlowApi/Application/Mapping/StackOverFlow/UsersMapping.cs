using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Domain.Entities.StackOverFlow.ValueObjects;
using Mapster;

namespace Application.Mapping.StackOverFlow;

public class UsersMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<UserDto, User>()
           .MapWith(el => User.Create(
                   (UserNumber)el.UserId,
                   el.DisplayName,
                   el.CreatedAt
               ));

    }
}
