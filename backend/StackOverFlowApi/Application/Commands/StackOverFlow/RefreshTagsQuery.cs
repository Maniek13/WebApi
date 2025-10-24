using Abstractions.DbContexts;
using MediatR;

namespace Application.Commands.StackOverFlow;

[Shared.Atributes.SaveDbContextAttribute(typeof(AbstractSOFDbContext))]
public record RefreshTagsQuery : IRequest
{
}
