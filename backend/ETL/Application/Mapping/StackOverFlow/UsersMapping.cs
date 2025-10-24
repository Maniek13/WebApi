using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Mapster;

namespace Application.Mapping.StackOverFlow;

public class UsersMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<UserDto, User>()
           .MapWith(el => User.Create(
                    el.UserId,
                    el.DisplayName,
                    el.CreatedAt
               ));

        cfg.NewConfig<User, UserDto>()
           .MapWith(el => new UserDto(
                    el.UserId,
                    el.DisplayName,
                    el.CreatedAt
               ));

    }
}
