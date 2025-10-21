using Application.Commands.StackOverFlow;
using Domain.Dtos.StackOverFlow;
using MassTransit;
using MediatR;

namespace Application.Consumers;

public class UsersConsumer : IConsumer<UserDto[]>
{
    private readonly IMediator _mediator;

    public UsersConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<UserDto[]> context)
    {
        await _mediator.Send(new AddOrUpdateUsersQuery { Users = context.Message });
    }
}
