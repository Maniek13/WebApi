using Application.Commands;
using FastEndpoints;
using MediatR;
using Presentation.Routes;

namespace Presentation.Endpoints;

internal class RefreshTags : EndpointWithoutRequest
{
    private readonly IMediator _mediator;

    public RefreshTags(IMediator mediator)
    {
        _mediator = mediator;
    }
    public override void Configure()
    {
        Put(TagsRoutes.Refresh);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _mediator.Send(new RefreshTagsQuery(), ct);
        await Send.OkAsync(ct);
    }
}
