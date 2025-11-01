using Application.Interfaces.App;
using Contracts.Requests.App;
using Contracts.Responses;
using FastEndpoints;
using Presentation.Routes.App;

namespace Presentation.Endpoints.App;

internal class LoginEndpoint : Endpoint<LoginRequest, TokenResponse>
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
        (string accesToken, string refreshToken) tokens = new();

        tokens = await _authService.LoginAsync(req.Name, req.Password, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");

        await Send.OkAsync(new TokenResponse(tokens.accesToken, tokens.refreshToken), ct);
    }
}
