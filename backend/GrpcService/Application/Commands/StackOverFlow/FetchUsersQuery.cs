using Domain.Dtos;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchUsersQuery : IRequest<UserDto[]>
{

}
