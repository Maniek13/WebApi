using Application.Commands;
using Contracts.Dtos;
using Contracts.Requests;
using FastEndpoints;
using MediatR;
using Shared.Pagination;

namespace Presentation.Endpoints;

internal class GetTags : Endpoint<GetTagsRequest, PagedList<TagDto>>
{
    private readonly IMediator _mediator;

    public GetTags(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("/api/tags/get");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTagsRequest req, CancellationToken ct)
    {
        var query = new GetTagsQuery
        {
            Page = req.Page,
            PageSize = req.PageSize,
            SortBy = req.SortBy,
            Descanding = req.Descanding,
        };

        var res = await _mediator.Send(query, ct);

        await Send.OkAsync(res);
    }
}
