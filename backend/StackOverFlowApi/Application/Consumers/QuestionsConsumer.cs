using Application.Commands.StackOverFlow;
using Contracts.Evetnts;
using MassTransit;
using MediatR;

namespace Application.Consumers;


public class QuestionsConsumer : IConsumer<QuestionEvent>
{
    private readonly IMediator _mediator;
    private readonly ISendEndpointProvider _bus;

    public QuestionsConsumer(IMediator mediator, ISendEndpointProvider bus)
    {
        _mediator = mediator;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<QuestionEvent> context)
    {
        await _mediator.Send(new AddOrUpdateQuestionsQuery { QuestionsWithNotExistedUsers = context.Message });
        //var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Test"));
        //await endpoint.Send(typeof(QuestionsConsumer), context.CancellationToken);
    }
}
