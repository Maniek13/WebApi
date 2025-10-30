using Abstractions.DbContexts;
using MediatR;

namespace Application.Commands.Api;

[Shared.Atributes.SaveDbContextAttribute(typeof(AbstractAppDbContext))]
public class MessageCommand : IRequest
{
    public string UserId { get; set; }
    public string Message { get; set; }
}
