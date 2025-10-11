
namespace Abstractions.Api;

public interface IStackOverFlowApiClient
{
    Task GetAsync(int page, int pageSize);
}
