using Abstractions.Api;
using Abstractions.ExternalApies;
using Contracts.Dtos;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Api;

public class StackOverFlowApiClient : IStackOverFlowApiClient
{
    private readonly HttpClient _httpClient;
    private readonly StackOverFlowOptions _options;

    public StackOverFlowApiClient(HttpClient httpClient, IOptions<StackOverFlowOptions> options)
    {
        _httpClient = httpClient;
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("EksploratorBot/1.0 (+https://twojastrona.pl)");
        _options = options.Value;
    }

    public async Task<TagDto[]> GetAsync(int page, int pageSize)
    {
        var response = await _httpClient.GetAsync($"{_options.BaseUrl}/tags?page={page}&pagesize={pageSize}&site=stackoverflow");
        var json = await response.Content.ReadAsStringAsync();

        var root = JObject.Parse(json);
        var itemsToken = root["items"];

        var result = itemsToken?.ToObject<TagDto[]>();

        return result == null ? Array.Empty<TagDto>() : result;
    }
}
