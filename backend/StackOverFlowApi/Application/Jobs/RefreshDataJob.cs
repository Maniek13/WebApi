using Application.Commands.StackOverFlow;
using MediatR;

namespace Application.Jobs;

public class RefreshDataJob
{
    private readonly IMediator _mediator;

    public RefreshDataJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RunAsync(CancellationToken cancellationToken) =>
        await _mediator.Send(new RefreshTagsQuery(), cancellationToken);
}
