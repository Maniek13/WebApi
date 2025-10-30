using Abstractions.Repositories.SOF;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class AddOrUpdateUsersHandler : IRequestHandler<AddOrUpdateUsersQuery>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AddOrUpdateUsersHandler(IUserRepository questionRepository, IMapper mapper)
    {
        _userRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddOrUpdateUsersQuery request, CancellationToken cancellationToken)
    {
        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(request.Users), cancellationToken);
    }
}
