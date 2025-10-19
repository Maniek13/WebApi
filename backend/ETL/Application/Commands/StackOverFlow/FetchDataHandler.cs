using Application.Api;
using MassTransit;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchDataHandler : IRequestHandler<FetchDataQuery>
{
    private readonly IMediator _mediator;
    private readonly IStackOverFlowApiClient _StackOverFlowApiClient;
    private readonly IBus _bus;

    public FetchDataHandler(IMediator mediator, IStackOverFlowApiClient stackOverFlowApiClient, IBus bus)
    {
        _mediator = mediator;
        _StackOverFlowApiClient = stackOverFlowApiClient;
        _bus = bus;
    }

    public async Task Handle(FetchDataQuery request, CancellationToken cancellationToken)
    {
        var questions = await _StackOverFlowApiClient.GetquestionsAsync(cancellationToken);

        await _mediator.Send(new AddOrUpdateQuestionsQuery { Questions = questions }, cancellationToken);

        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Questions"));

        await endpoint.Send(questions, cancellationToken);
    }
}
