using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class AddOrUpdateQuestionsHandler : IRequestHandler<AddOrUpdateQuestionsQuery>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AddOrUpdateQuestionsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddOrUpdateQuestionsQuery request, CancellationToken cancellationToken)
    {
        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(request.QuestionsWithNotExistedUsers.Users), cancellationToken);
        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<QuestionDto[], List<Question>>(request.QuestionsWithNotExistedUsers.Questions), cancellationToken);
    }
}
