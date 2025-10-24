using Application.Interfaces.App;
using Contracts.Requests.App;
using Contracts.Responses;
using FastEndpoints;
using Presentation.Routes.App;

namespace Presentation.Endpoints.App;

internal class RefreshTokenEndpoint : Endpoint<RefreshTokenRequest, TokenResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post(UserRoutes.Refresh);
        AllowAnonymous();
    }

    public override async Task HandleAsync(RefreshTokenRequest req, CancellationToken ct)
    {
        (string accesToken, string refreshToken) tokens = new();
        try
        {
            tokens = await _authService.RefreshTokenAsync(req.RefreshToken, HttpContext.Connection.RemoteIpAddress?.ToString() ?? "");
        }
        catch (UnauthorizedAccessException ex)
        {
            ThrowError(ex.Message, 401);
        }

        await Send.OkAsync(new TokenResponse(tokens.accesToken, tokens.refreshToken), ct);
    }
}
