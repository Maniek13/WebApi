using Abstarction.Repositories;
using Application.Api;
using Domain.Dtos;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;
using NHibernate;

namespace Application.Commands.StackOverFlow;

public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto?>
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public GetUserHandler(IMapper mapper,
        IStackOverFlowApiClient stackOverFlowApiClient,
        IUserRepository userRepository,
        ISession session)
    {
        _mapper = mapper;
        _stackOverFlowApiClient = stackOverFlowApiClient;
        _userRepository = userRepository;
        _session = session;
    }

    public async Task<UserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _stackOverFlowApiClient.GetUserAsync(request.UserId, cancellationToken);

        if(user != null) { }
            _userRepository.Add(_mapper.Map<UserDto, User>(user));
        
        return user;
    }
}
