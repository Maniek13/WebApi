using Application.Commands;
using Contracts.Dtos;
using Contracts.Requests;
using Domain.Entities;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Presentation.Routes;
using Shared.Pagination;
using System.ComponentModel.DataAnnotations.Schema;

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
