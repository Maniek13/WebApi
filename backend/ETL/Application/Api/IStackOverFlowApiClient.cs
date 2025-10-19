using Contracts.Dtos.StackOverFlow;

namespace Application.Api;

public interface IStackOverFlowApiClient
{
    Task<QuestionDto[]> GetquestionsAsync(CancellationToken ct);
}
