using Application.Commands.StackOverFlow;
using Contracts.Evetnts;
using MassTransit;
using MediatR;

namespace Application.Consumers;

public class UsersConsumer : IConsumer<UserEvent>
{
    private readonly IMediator _mediator;

    public UsersConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserEvent> context)
    {
        await _mediator.Send(new AddOrUpdateUsersQuery { Users = context.Message.Users });
    }
}
