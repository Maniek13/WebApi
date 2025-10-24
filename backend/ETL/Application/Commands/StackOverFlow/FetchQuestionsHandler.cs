using Abstractions.Interfaces;
using Abstractions.Repositories;
using Application.Api;
using Contracts.Dtos.StackOverFlow;
using Contracts.Evetnts;
using Domain.Entities.StackOverFlow;
using Infrastructure.Api;
using MapsterMapper;
using MassTransit;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchQuestionsHandler : IRequestHandler<FetchQuestionsQuery>
{
    private readonly IStackOverFlowApiClient _StackOverFlowApiClient;
    private readonly ISendEndpointProvider  _bus;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISOFGrpcClient _sOFGrpcClient;
    private readonly IUnitOfWork _unitOfWork;

    public FetchQuestionsHandler(IStackOverFlowApiClient stackOverFlowApiClient, ISendEndpointProvider bus, IQuestionRepository questionRepository, IUserRepository userRepository, IMapper mapper, ISOFGrpcClient sOFGrpcClient, IUnitOfWork unitOfWork)
    {
        _StackOverFlowApiClient = stackOverFlowApiClient;
        _bus = bus;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _sOFGrpcClient = sOFGrpcClient;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(FetchQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = (await _StackOverFlowApiClient.GetquestionsAsync(cancellationToken)).ToList();

        var questionsWithoutUsers = questions.Where(el => el.Member.UserId != null && !_userRepository.CheckUserExist((long)el.Member.UserId)).ToArray();

        long[] userIds = questionsWithoutUsers.Select(el => (long)el.Member.UserId!).ToArray();

        var users = await _sOFGrpcClient.GetUsersByIdsAsync(userIds, cancellationToken);

        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(users), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        List<Question> questionsToAddOrUpdate = [];

        List<QuestionDto> questionsToUpdate = questions.Where(el => el.Member.UserId != null && _userRepository.CheckUserExist((long)el.Member.UserId)).ToList();
        questionsToAddOrUpdate.AddRange(_mapper.Map<List<QuestionDto>, List<Question>>(questionsToUpdate));

        List<QuestionDto> questionsWithDeleteUser = questions.Where(el => !questionsToUpdate.Any(item => item.QuestionId == el.QuestionId)).ToList();
        questionsToAddOrUpdate.AddRange(_mapper.Map<(List<QuestionDto>, long? userId), List<Question>>((questionsWithDeleteUser, null)));
        questionsWithDeleteUser = _mapper.Map<(List<QuestionDto>, long? userId), List<QuestionDto>>((questionsWithDeleteUser, null));

        await _questionRepository.AddOrUpdateQuestionsAsync(questionsToAddOrUpdate, cancellationToken);

        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Questions"));
        await endpoint.Send(new QuestionEvent { Users = users, Questions = [.. questionsToUpdate.Concat(questionsWithDeleteUser)] }, cancellationToken);
    }
}
