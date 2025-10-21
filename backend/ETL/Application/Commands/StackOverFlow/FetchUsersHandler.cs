using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Infrastructure.Api;
using MapsterMapper;
using MassTransit;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchUsersHandler : IRequestHandler<FetchUsersQuery>
{
    private readonly IMediator _mediator;
    private readonly ISOFGrpcClient _sOFGrpcClient;
    private readonly IBus _bus;
    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;

    public FetchUsersHandler(IMediator mediator, ISOFGrpcClient sOFGrpcClient, IBus bus, IUserRepository iUserRepository, IMapper mapper)
    {
        _mediator = mediator;
        _sOFGrpcClient = sOFGrpcClient;
        _bus = bus;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
    }

    public async Task Handle(FetchUsersQuery request, CancellationToken cancellationToken)
    {
        var questions = await _sOFGrpcClient.GetUsersAsync(cancellationToken);

        await _iUserRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(questions), cancellationToken);


        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Users"));

        await endpoint.Send(questions, cancellationToken);
    }
}
