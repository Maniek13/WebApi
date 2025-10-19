using Application.Commands.StackOverFlow;
using MediatR;

namespace Application.Jobs;

public class FetchQuestionsJob
{
    private readonly IMediator _mediator;

    public FetchQuestionsJob(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RunAsync(CancellationToken cancellationToken) =>
       await _mediator.Send(new FetchDataQuery(), cancellationToken);
}
