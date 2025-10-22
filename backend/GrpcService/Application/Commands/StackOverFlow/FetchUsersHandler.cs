using Abstarction.Repositories;
using Application.Api;
using Domain.Dtos;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;
using NHibernate;

namespace Application.Commands.StackOverFlow;

public class FetchDataHandler : IRequestHandler<FetchUsersQuery, UserDto[]>
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISession _session;

    public FetchDataHandler(IMapper mapper,
        IStackOverFlowApiClient stackOverFlowApiClient,
        IUserRepository userRepository,
        ISession session)
    {
        _mapper = mapper;
        _stackOverFlowApiClient = stackOverFlowApiClient;
        _userRepository = userRepository;
        _session = session;
    }

    public async Task<UserDto[]> Handle(FetchUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _stackOverFlowApiClient.GetUsersAsync(cancellationToken);

        using var transaction = _session.BeginTransaction();

        try
        {
            for (int i = 0; i < users.Count(); ++i)
                _userRepository.Add(_mapper.Map<UserDto, User>(users[i]));

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
        }
        
        return users;
    }
}
