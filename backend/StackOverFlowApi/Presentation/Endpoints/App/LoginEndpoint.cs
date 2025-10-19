using Application.Interfaces.App;
using Contracts.Requests.App;
using Contracts.Results;
using FastEndpoints;
using Presentation.Routes.App;

namespace Presentation.Endpoints.App;

internal class LoginEndpoint : Endpoint<LoginRequest, LoginResult>
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
        try
        {
            tokens = await _authService.LoginAsync(req.Name, req.Password, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");
        }
        catch (ArgumentException ex)
        {
            ThrowError(ex.Message, 400);
        }

        await Send.OkAsync(new LoginResult(tokens.accesToken, tokens.refreshToken), ct);
    }
}
