using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MassTransit;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class AddOrUpdateQuestionsHandler : IRequestHandler<AddOrUpdateQuestionsQuery>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUsersRepositoryRO _usersRepositoryRO;
    private readonly IMapper _mapper; 

    public AddOrUpdateQuestionsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IUsersRepositoryRO usersRepositoryRO, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _usersRepositoryRO = usersRepositoryRO;
        _mapper = mapper;
    }

    public async Task Handle(AddOrUpdateQuestionsQuery request, CancellationToken cancellationToken)
    {
        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(request.QuestionsWithNotExistedUsers.Users), cancellationToken);

        var questions = request.QuestionsWithNotExistedUsers.Questions.Where(el => el.Member == null || _usersRepositoryRO.CheckIfUserExistByUserId(el.Member.UserId)).ToArray();

        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<QuestionDto[], List<Question>>(questions), cancellationToken);
    }
}
