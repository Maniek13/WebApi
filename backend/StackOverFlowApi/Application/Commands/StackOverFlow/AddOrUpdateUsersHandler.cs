using Abstractions.Repositories;
using Domain.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class AddOrUpdateUsersHandler : IRequestHandler<AddOrUpdateUsersQuery>
{
    private readonly IUserRepository _questionRepository;
    private readonly IMapper _mapper;

    public AddOrUpdateUsersHandler(IUserRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddOrUpdateUsersQuery request, CancellationToken cancellationToken)
    {
        await _questionRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(request.Users), cancellationToken);
    }
}
