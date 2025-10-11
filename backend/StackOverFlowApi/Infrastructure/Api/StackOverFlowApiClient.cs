using Abstractions.Api;
using Configuration.ExternalApies;
using Microsoft.Extensions.Options;

namespace Infrastructure.Api;

public class StackOverFlowApiClient : IStackOverFlowApiClient
{
    private readonly HttpClient _httpClient;
    private readonly StackOverFlowOptions _options;

    public StackOverFlowApiClient(HttpClient httpClient, IOptions<StackOverFlowOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task GetAsync(int page, int pageSize)
    {

        var response = await _httpClient.GetAsync($"{_options.BaseUrl}/tags?page={page}&pagesize={pageSize}&site=stackoverflow");
        throw new NotImplementedException();
    }
}
