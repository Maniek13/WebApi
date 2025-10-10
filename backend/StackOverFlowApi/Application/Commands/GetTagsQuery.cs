using Domain.Entities;
using MediatR;

namespace Application.Commands;

public record GetTagsQuery : IRequest<Tag[]>
{
}
