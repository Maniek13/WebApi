using Abstractions.DbContexts;
using Domain.Dtos.StackOverFlow;
using MediatR;
using Shared.Pagination;

namespace Application.Commands.StackOverFlow;

[Shared.Atributes.DbContextAtribute(typeof(AbstractSOFDbContext))]
public record GetTagsQuery : IRequest<PagedList<TagDto>>
{
    public int Page {  get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public string SortBy { get; set; } = "Id";
    public bool Descending { get; set; } = false;
}
