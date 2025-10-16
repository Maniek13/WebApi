using Application.Interfaces.App;
using Contracts.Requests.App;
using FastEndpoints;
using Presentation.Routes.App;

namespace Presentation.Endpoints.App;

internal class RegisterEndpoint : Endpoint<RegisterRequest>
{
    private readonly IAuthService _authService;

    public RegisterEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post(UserRoutes.Register);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        await _authService.CreateUserAsync(req.Name, req.Password, req.Role);

        await Send.OkAsync(ct);
    }
}
