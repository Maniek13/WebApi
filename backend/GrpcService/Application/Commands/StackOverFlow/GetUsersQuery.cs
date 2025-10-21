using Domain.Dtos;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class GetUsersQuery : IRequest<UserDto[]>
{
}
