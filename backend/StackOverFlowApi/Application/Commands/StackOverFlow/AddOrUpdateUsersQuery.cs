using Abstractions.DbContexts;
using Contracts.Dtos.StackOverFlow;
using MediatR;

namespace Application.Commands.StackOverFlow
{
    [Shared.Atributes.SaveDbContextAttribute(typeof(AbstractAppDbContext))]
    public class AddOrUpdateUsersQuery : IRequest
    {
        public UserDto[] Users { get; set; } = [];
    }

}
