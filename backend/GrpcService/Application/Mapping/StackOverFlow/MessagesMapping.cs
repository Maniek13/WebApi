using Contracts.Messages;
using Domain.Dtos;
using Mapster;

namespace Application.Mapping.StackOverFlow;

public class MessagesMapping : IRegister
{
    public void Register(TypeAdapterConfig cfg)
    {
        cfg.NewConfig<UserDto, User>()
           .MapWith(el => new User { UserId = el.UserId, DisplayName = el.DispalaName, CreationDate = el.CreatedAt });
    }
}
