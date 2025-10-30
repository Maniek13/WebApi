using Domain.Entities.StackOverFlow;

namespace Abstractions.Repositories.SOF;

public interface IQuestionRepository
{
    public Task AddOrUpdateQuestionsAsync(List<Question> questions, CancellationToken ct);
}
