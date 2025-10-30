using Application.Commands.Api;
using Application.Interfaces.App;
using Contracts.Requests.App;
using Contracts.Responses;
using FastEndpoints;
using MassTransit.Mediator;
using Presentation.Routes.App;
using System.Security.Claims;

namespace Presentation.Endpoints.App;

internal class SetAddressEndpoint : Endpoint<AddressRequest, TokenResponse>
{
    private readonly IMediator _mediator;

    public SetAddressEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post(UserRoutes.SetAddress);

        Policies("IsUser");
    }

    public override async Task HandleAsync(AddressRequest req, CancellationToken ct)
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var query = new SetUserAddressCommand
        {
            UserId = userId!,
            City = req.City,
            Street = req.Street,
            ZipCode = req.ZipCode,
        };

        await _mediator.Send(query);  

        await Send.OkAsync(ct);
    }
}
