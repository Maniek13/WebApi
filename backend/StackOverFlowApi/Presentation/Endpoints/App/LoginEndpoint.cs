using Application.Interfaces.App;
using Contracts.Requests.App;
using FastEndpoints;
using Presentation.Routes.App;

namespace Presentation.Endpoints.App;

internal class LoginEndpoint : Endpoint<LoginRequest, string?>
{
    private readonly IAuthService _authService;

    public LoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post(UserRoutes.Login);
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        string? token = null;
        try
        {
            token = await _authService.LoginAsync(req.Name, req.Password);
        }
        catch (ArgumentException ex)
        {
            ThrowError(ex.Message, 400);
        }

        await Send.OkAsync(token, ct);
    }
}
