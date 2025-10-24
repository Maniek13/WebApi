using Domain.Dtos.StackOverFlow;

namespace Abstractions.Api;

public interface IStackOverFlowApiClient
{
    Task<TagDto[]> GetTagsAsync();
}
