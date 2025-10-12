using Contracts.Dtos;

namespace Abstractions.Api;

public interface IStackOverFlowApiClient
{
    Task<TagDto[]> GetTagsAsync();
}
