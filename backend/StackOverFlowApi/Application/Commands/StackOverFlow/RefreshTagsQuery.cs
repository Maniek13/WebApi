using Abstractions.DbContexts;
using MediatR;

namespace Application.Commands.StackOverFlow;

[Shared.Atributes.DbContextAtribute(typeof(AbstractSOFDbContext))]
public record RefreshTagsQuery : IRequest
{
}
