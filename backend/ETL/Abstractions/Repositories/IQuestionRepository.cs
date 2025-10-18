using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories;

public interface IQuestionRepository
{
    public Task AddOrUpdateQuestionsAsync(List<Question> questions, CancellationToken ct);
}
