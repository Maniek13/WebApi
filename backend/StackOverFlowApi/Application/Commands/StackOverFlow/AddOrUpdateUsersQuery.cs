using Domain.Dtos.StackOverFlow;
using MediatR;

namespace Application.Commands.StackOverFlow
{
    public class AddOrUpdateUsersQuery : IRequest
    {
        public UserDto[] Users { get; set; } = [];
    }

}
