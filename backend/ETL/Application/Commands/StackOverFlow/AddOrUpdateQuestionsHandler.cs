using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class AddOrUpdateQuestionsHandler : IRequestHandler<AddOrUpdateQuestionsQuery>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public AddOrUpdateQuestionsHandler(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public async Task Handle(AddOrUpdateQuestionsQuery request, CancellationToken cancellationToken)
    {
        await _questionRepository.AddOrUpdateQuestionsAsync(_mapper.Map<QuestionDto[], List<Question>>(request.Questions), cancellationToken);
    }
}
