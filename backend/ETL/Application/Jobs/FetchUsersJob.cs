using Application.Commands.StackOverFlow;
using MediatR;

namespace Application.Jobs;

public class FetchUsersJob
{
    private readonly IMediator _mediator;

    public FetchUsersJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RunAsync(CancellationToken cancellationToken) =>
       await _mediator.Send(new FetchUsersQuery(), cancellationToken);
}
