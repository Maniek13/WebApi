using Domain.Dtos;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class GetUsersByIdsQuery : IRequest<UserDto[]>
{
    public long[] UserIds { get; set; }
}
