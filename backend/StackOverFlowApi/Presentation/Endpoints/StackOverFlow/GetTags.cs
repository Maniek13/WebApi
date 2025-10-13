using Application.Commands.StackOverFlow;
using Contracts.Dtos.StackOverFlow;
using Contracts.Requests.StackOverFlow;
using Domain.Entities.StackOverFlow;
using FastEndpoints;
using MediatR;
using Presentation.Routes.StackOverFlow;
using Shared.Pagination;

namespace Presentation.Endpoints.StackOverFlow;

internal class GetTags : Endpoint<GetTagsRequest, PagedList<TagDto>>
{
    private readonly IMediator _mediator;

    public GetTags(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get(TagsRoutes.Get);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTagsRequest req, CancellationToken ct)
    {
        if (!Tag.CheckHavePropertyByName(req.SortBy))
            ThrowError($"Property {req.SortBy} doesn't exist in type Tag", 400);

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
