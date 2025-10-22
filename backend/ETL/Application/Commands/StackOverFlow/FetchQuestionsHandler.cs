using Abstractions.Repositories;
using Application.Api;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using Infrastructure.Api;
using MapsterMapper;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Application.Commands.StackOverFlow;

public class FetchQuestionsHandler : IRequestHandler<FetchQuestionsQuery>
{
    private readonly IMediator _mediator;
    private readonly IStackOverFlowApiClient _StackOverFlowApiClient;
    private readonly IBus _bus;
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ISOFGrpcClient _sOFGrpcClient;

    public FetchQuestionsHandler(IMediator mediator, IStackOverFlowApiClient stackOverFlowApiClient, IBus bus, IQuestionRepository questionRepository, IUserRepository userRepository, IMapper mapper, ISOFGrpcClient sOFGrpcClient)
    {
        _mediator = mediator;
        _StackOverFlowApiClient = stackOverFlowApiClient;
        _bus = bus;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _sOFGrpcClient = sOFGrpcClient;
    }

    public async Task Handle(FetchQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = (await _StackOverFlowApiClient.GetquestionsAsync(cancellationToken)).ToList();

        var questionsWithoutUsers = questions.Where(el => !_userRepository.CheckUserExist(el.Member.UserId, cancellationToken)).ToArray();

        List<User> usersToAdd = new List<User>();

        for(int i = 0; i < questionsWithoutUsers.Length; ++i)
        {
            var userId = questionsWithoutUsers[i].Member.UserId;
            var user = await _sOFGrpcClient.GetUserAsync(userId, cancellationToken);

            if (user == null)
                continue;

            usersToAdd.Add(_mapper.Map<UserDto, User>(await _sOFGrpcClient.GetUserAsync(userId, cancellationToken)));
            questions.Remove(questions.First(el => el.Member.UserId == userId));
        }

        await _userRepository.AddOrUpdateUsersAsync(usersToAdd, cancellationToken);

        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<List<QuestionDto>, List<Question>>(questions), cancellationToken);

        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Questions"));

        await endpoint.Send(new FechQuestionDto(_mapper.Map<List<User>, UserDto[]>(usersToAdd), questions.ToArray()), cancellationToken);
    }
}
