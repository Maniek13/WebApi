using Application.Commands;
using Domain.Entities;
using FastEndpoints;
using MediatR;

namespace Presentation.Endpoints
{
    internal class GetTags : EndpointWithoutRequest<Tag[]>
    {
        private readonly IMediator _mediator;

        public GetTags(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Get("/api/tags/get");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var query = new GetTagsQuery
            {

            };

            var res = await _mediator.Send(query, ct);

            await Send.OkAsync(res);
        }
    }
}
