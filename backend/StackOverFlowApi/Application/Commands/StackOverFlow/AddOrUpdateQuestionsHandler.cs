using Abstractions.DbContexts;
using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.StackOverFlow;

public class AddOrUpdateQuestionsHandler : IRequestHandler<AddOrUpdateQuestionsQuery>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppUi



    public async Task Handle(AddOrUpdateQuestionsQuery request, CancellationToken cancellationToken)
    {
        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(request.QuestionsWithNotExistedUsers.Users), cancellationToken);
        
        
        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<QuestionDto[], List<Question>>(request.QuestionsWithNotExistedUsers.Questions), cancellationToken);
    }
}
