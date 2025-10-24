using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Contracts.Evetnts;
using Domain.Entities.StackOverFlow;
using Infrastructure.Api;
using MapsterMapper;
using MassTransit;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchUsersHandler : IRequestHandler<FetchUsersQuery>
{
    private readonly ISOFGrpcClient _sOFGrpcClient;
    private readonly ISendEndpointProvider _bus;
    private readonly IUserRepository _iUserRepository;
    private readonly IMapper _mapper;

    public FetchUsersHandler(ISOFGrpcClient sOFGrpcClient, ISendEndpointProvider  bus, IUserRepository iUserRepository, IMapper mapper)
    {
        _sOFGrpcClient = sOFGrpcClient;
        _bus = bus;
        _iUserRepository = iUserRepository;
        _mapper = mapper;
    }

    public async Task Handle(FetchUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _sOFGrpcClient.GetUsersAsync(cancellationToken);
        var mappedUsers = _mapper.Map<UserDto[], List<User>>(users);

        await _iUserRepository.AddOrUpdateUsersAsync(mappedUsers, cancellationToken);

        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Users"));

        await endpoint.Send(new UserEvent { Users = users }, cancellationToken);
    }
}
