using Application.Api;
using Domain.Dtos;
using Infrastructure.Api.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Infrastructure.Api;

public class StackOverFlowApiClient : IStackOverFlowApiClient
{
    private readonly HttpClient _httpClient;
    private readonly StackOverFlowOptions _options;

    public StackOverFlowApiClient(HttpClient httpClient, IOptions<StackOverFlowOptions> options)
    {
        _httpClient = httpClient;
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("GRPC");
        _options = options.Value;
    }

    public async Task<UserDto[]> GetUsersAsync(CancellationToken ct)
    {
        long itemsCount = _options.Data.UsersCount;
        long pagesCount = itemsCount % 100 != 0 ? itemsCount / 100 + 1 : itemsCount / 100;

        List<UserDto> users = [];

        for (int i = 1; i <= pagesCount; ++i)
        {
            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/users?page={i}&pagesize={100}&order=desc&sort=creation&site=stackoverflow", ct);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                break;

            var json = await response.Content.ReadAsStringAsync(ct);
            var root = JObject.Parse(json);

            var errorId = root["error_id"];
            if (errorId != null && errorId.ToObject<int>().Equals(502))
                break;

            var itemsToken = root["items"]!;
            var listDto = itemsToken.ToObject<List<UserDto>>()!;
            users.AddRange(listDto!);
        }



        return users.DistinctBy(u => u.UserId).ToArray();
    }

    public async Task<UserDto?> GetUserAsync(long userId, CancellationToken ct)
    {
        long itemsCount = _options.Data.UsersCount;
        long pagesCount = itemsCount % 100 != 0 ? itemsCount / 100 + 1 : itemsCount / 100;

        List<UserDto> users = [];

        for (int i = 1; i <= pagesCount; ++i)
        {
            var response = await _httpClient.GetAsync($"{_options.BaseUrl}/users?{userId}&site=stackoverflow", ct);

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
                break;

            var json = await response.Content.ReadAsStringAsync(ct);
            var root = JObject.Parse(json);

            var errorId = root["error_id"];
            if (errorId != null && errorId.ToObject<int>().Equals(502))
                break;

            var itemsToken = root["items"]!;
            var listDto = itemsToken.ToObject<List<UserDto>>()!;
            users.AddRange(listDto!);
        }

        if(users == null || users.Count == 0 )
            throw new ArgumentException("User doesn't exist");

        return users.FirstOrDefault();
    }
}
