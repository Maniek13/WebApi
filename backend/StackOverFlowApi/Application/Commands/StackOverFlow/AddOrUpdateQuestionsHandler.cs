using Abstractions.DbContexts;
using Abstractions.Interfaces;
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
    private readonly IUnitOfWork<AbstractSOFDbContext> _sofUnitOfWork;

    public AddOrUpdateQuestionsHandler(IQuestionRepository questionRepository, IUserRepository userRepository, IMapper mapper, IUnitOfWork<AbstractSOFDbContext> sofUnitOfWork)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _sofUnitOfWork = sofUnitOfWork;
    }

    public async Task Handle(AddOrUpdateQuestionsQuery request, CancellationToken cancellationToken)
    {
        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(request.QuestionsWithNotExistedUsers.Users), cancellationToken);
        await _sofUnitOfWork.SaveChangesAsync(cancellationToken);

        var questions = request.QuestionsWithNotExistedUsers.Questions.Where(el => el.Member.UserId == null || _userRepository.CheckIfUserExistByUserId((long)el.Member.UserId)).ToArray();
        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<QuestionDto[], List<Question>>(questions), cancellationToken);
    }
}
