using Abstarction.Repositories;
using Application.Api;
using Domain.Dtos;
using MapsterMapper;
using MediatR;
using NHibernate;

namespace Application.Commands.StackOverFlow;

public class GetUsersByIdsHandler : IRequestHandler<GetUsersByIdsQuery, UserDto[]>
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public GetUsersByIdsHandler(IMapper mapper,
        IStackOverFlowApiClient stackOverFlowApiClient,
        IUserRepository userRepository,
        ISession session)
    {
        _mapper = mapper;
        _stackOverFlowApiClient = stackOverFlowApiClient;
        _userRepository = userRepository;
        _session = session;
    }

    public async Task<UserDto[]> Handle(GetUsersByIdsQuery request, CancellationToken cancellationToken)
    {
        var users = await _stackOverFlowApiClient.GetUsersByIdsAsync(request.UserIds.ToList(), cancellationToken);

        await _userRepository.AddOrUpdateAsync(_mapper.Map <UserDto[], List<Domain.Entities.StackOverFlow.User>>(users));
    
        return users;
    }
}
