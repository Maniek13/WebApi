using Contracts.Dtos;

namespace Abstractions.Api;

public interface IStackOverFlowApiClient
{
    Task<TagDto[]> GetAsync(int page, int pageSize);
}
