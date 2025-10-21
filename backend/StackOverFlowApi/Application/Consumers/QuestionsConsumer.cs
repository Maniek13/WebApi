using Application.Commands.StackOverFlow;
using Domain.Dtos.StackOverFlow;
using MassTransit;
using MediatR;

namespace Application.Consumers;

public class QuestionsConsumer : IConsumer<QuestionDto[]>
{
    private readonly IMediator _mediator;

    public QuestionsConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<QuestionDto[]> context)
    {
        await _mediator.Send(new AddOrUpdateQuestionsQuery { Questions = context.Message });
    }
}
