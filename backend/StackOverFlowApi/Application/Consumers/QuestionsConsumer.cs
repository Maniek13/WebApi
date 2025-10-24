using Application.Commands.StackOverFlow;
using Contracts.Evetnts;
using MassTransit;
using MediatR;

namespace Application.Consumers;


public class QuestionsConsumer : IConsumer<QuestionEvent>
{
    private readonly IMediator _mediator;

    public QuestionsConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<QuestionEvent> context)
    {
        await _mediator.Send(new AddOrUpdateQuestionsQuery { QuestionsWithNotExistedUsers = context.Message });
    }
}
