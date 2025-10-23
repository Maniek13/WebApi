using Abstractions.DbContexts;
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
    private readonly IBus _bus;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISOFGrpcClient _sOFGrpcClient;
    private readonly AbstractSOFDbContext _dbContext;

    public FetchQuestionsHandler(IStackOverFlowApiClient stackOverFlowApiClient, IBus bus, IQuestionRepository questionRepository, IUserRepository userRepository, IMapper mapper, ISOFGrpcClient sOFGrpcClient, AbstractSOFDbContext dbContext)
    {
        _StackOverFlowApiClient = stackOverFlowApiClient;
        _bus = bus;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _sOFGrpcClient = sOFGrpcClient;
        _dbContext = dbContext;
    }

    public async Task Handle(FetchQuestionsQuery request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        var questions = (await _StackOverFlowApiClient.GetquestionsAsync(cancellationToken)).ToList();

        var questionsWithoutUsers = questions.Where(el => !_userRepository.CheckUserExist(el.Member.UserId)).ToArray();

        long[] userIds = questionsWithoutUsers.Select(el => el.Member.UserId).ToArray();
        var users = await _sOFGrpcClient.GetUsersByIdsAsync(userIds, cancellationToken);

        await _userRepository.AddOrUpdateUsersAsync(_mapper.Map<UserDto[], List<User>>(users), cancellationToken);

        List<Question> questionsToAddOrUpdate = [];

        List<QuestionDto> questionsToUpdate = questions.Where(el => _userRepository.CheckUserExist(el.Member.UserId)).ToList();
        questionsToAddOrUpdate.AddRange(_mapper.Map<List<QuestionDto>, List<Question>>(questionsToUpdate));

        List<QuestionDto> questionsWithDeleteUser = questions.Where(el => !questionsToUpdate.Any(item => item.QuestionId == el.QuestionId)).ToList();
        questionsToAddOrUpdate.AddRange(_mapper.Map<(List<QuestionDto>, long? userId), List<Question>>((questionsWithDeleteUser, null)));

        await _questionRepository.AddOrUpdateQuestionsAsync(questionsToAddOrUpdate, cancellationToken);

        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Questions"));
        await endpoint.Send(new QuestionEvent { Users = users, Questions = [.. questionsToUpdate.Concat(questionsWithDeleteUser)] }, cancellationToken);

        await transaction.CommitAsync();
    }
}
