using FastEndpoints;

namespace Presentation.Endpoints
{
    internal class GetTags : EndpointWithoutRequest
    {
        public override void Configure()
        {
            Post("/api/tags/get");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            await Send.OkAsync();
        }
    }
}
