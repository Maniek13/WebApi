using Application.Commands.Api;
using Contracts.Requests.App;
using FastEndpoints;
using MediatR;
using Presentation.Routes.App;
using System.Security.Claims;

namespace Presentation.Endpoints.App;

internal class AddMessageEndpoint : Endpoint<AddMessageRequest>
{
    private readonly IMediator _mediator;

    public AddMessageEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(UserRoutes.AddMessage);
        Policies("IsUser");
    }

    public override async Task HandleAsync(AddMessageRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var query = new MessageCommand
        {
            UserId = userId!,
            Message = req.Message,
        };

        await _mediator.Send(query);  

        await Send.OkAsync(ct);
    }
}
