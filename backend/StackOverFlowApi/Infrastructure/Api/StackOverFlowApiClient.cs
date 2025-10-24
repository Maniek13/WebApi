using Abstractions.Api;
using Domain.Dtos.StackOverFlow;
using Infrastructure.Api.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Serilog;
using System.Net;

namespace Infrastructure.Api;

public class StackOverFlowApiClient : IStackOverFlowApiClient
{
    private readonly HttpClient _httpClient;
    private readonly StackOverFlowOptions _options;

    public StackOverFlowApiClient(HttpClient httpClient, IOptions<StackOverFlowOptions> options)
    {
        _httpClient = httpClient;
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("WebApi/1.0");
        _options = options.Value;
    }

    public async Task<TagDto[]> GetTagsAsync()
    {
        long tagCount = _options.Data.TagsCount;

        long pagesCount = tagCount % 100 != 0 ? tagCount / 100 + 1 : tagCount / 100;

        List<TagDto> tags = [];

        for (int i = 1; i <= pagesCount; ++i)
        {
            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/tags?page={i}&pagesize={100}&site=stackoverflow");

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                Task.Run(() =>Log.Error("Data could not be loaded because there were too many requests to the Stack Overflow API. Please refresh data. Error code: {errorCode}, error message: {errorMessage}", (int)response.StatusCode, response.RequestMessage));
                break;
            }

            var json = await response.Content.ReadAsStringAsync();
            var root = JObject.Parse(json);

            var errorId = root["error_id"];
            if (errorId != null && errorId.ToObject<int>().Equals(502))
            {
                Task.Run(() => Log.Error("Data could not be loaded because there were too many requests to the Stack Overflow API. Please refresh data. Error code: {errorCode}, error message: {errorMessage}", (int)response.StatusCode, root["error_message"]?.ToObject<string>()));
                break;
            }

            var itemsToken = root["items"]!;
            var listDto = itemsToken.ToObject<List<TagDto>>()!;
            tags.AddRange(listDto!);
        }


        return tags.DistinctBy(el => el.Name).ToArray();
    }
}
