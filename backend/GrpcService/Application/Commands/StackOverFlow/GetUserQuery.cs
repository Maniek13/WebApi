using Domain.Dtos;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class GetUserQuery : IRequest<UserDto?>
{
    public long UserId { get; set; }
}
