using Abstractions.Repositories;
using Application.Api;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MassTransit;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchQuestionsHandler : IRequestHandler<FetchQuestionsQuery>
{
    private readonly IMediator _mediator;
    private readonly IStackOverFlowApiClient _StackOverFlowApiClient;
    private readonly IBus _bus;
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public FetchQuestionsHandler(IMediator mediator, IStackOverFlowApiClient stackOverFlowApiClient, IBus bus, IQuestionRepository questionRepository, IMapper mapper)
    {
        _mediator = mediator;
        _StackOverFlowApiClient = stackOverFlowApiClient;
        _bus = bus;
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task Handle(FetchQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await _StackOverFlowApiClient.GetquestionsAsync(cancellationToken);

        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<QuestionDto[], List<Question>>(questions), cancellationToken);


        var endpoint = await _bus.GetSendEndpoint(new Uri("queue:Questions"));

        await endpoint.Send(questions, cancellationToken);
    }
}
