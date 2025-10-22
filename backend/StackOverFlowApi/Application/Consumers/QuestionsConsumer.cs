using Application.Commands.StackOverFlow;
using Contracts.Dtos.StackOverFlow;
using MassTransit;
using MediatR;

namespace Application.Consumers;

public class QuestionsConsumer : IConsumer<FechQuestionDto>
{
    private readonly IMediator _mediator;

    public QuestionsConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<FechQuestionDto> context)
    {
        await _mediator.Send(new AddOrUpdateQuestionsQuery { QuestionsWithNotExistedUsers = context.Message });
    }
}
