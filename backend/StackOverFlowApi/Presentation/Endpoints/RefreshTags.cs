using FastEndpoints;

namespace Presentation.Endpoints
{
    internal class RefreshTags : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Put("/api/tags/refresh");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
