using Contracts.Dtos.StackOverFlow;
using MediatR;
using Shared.Pagination;

namespace Application.Commands.StackOverFlow;

public record GetTagsQuery : IRequest<PagedList<TagDto>>
{
    public int Page {  get; set; } = 1;
    public int PageSize { get; set; } = 100;
    public string SortBy { get; set; } = "Id";
    public bool Descanding { get; set; } = false;
}
