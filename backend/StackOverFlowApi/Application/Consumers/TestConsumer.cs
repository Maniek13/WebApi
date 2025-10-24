using Application.Commands.StackOverFlow;
using Contracts.Evetnts;
using MassTransit;
using MediatR;

namespace Application.Consumers;

public class TestConsumer : IConsumer<Type>
{

    public TestConsumer(IMediator mediator)
    {
    }

    public async Task Consume(ConsumeContext<Type> context)
    {
    }
}
