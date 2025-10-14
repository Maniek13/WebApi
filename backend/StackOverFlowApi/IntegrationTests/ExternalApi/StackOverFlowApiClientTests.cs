using Abstractions.Api;
using FluentAssertions;
using Infrastructure.Api;
using Infrastructure.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Helpers;

namespace IntegrationTests.ExternalApi;

public class StackOverFlowApiClientTests
{
    private readonly Random _random = new Random();
    private readonly IServiceProvider _provider;
    private readonly IConfiguration _configuration;
    public StackOverFlowApiClientTests()
    {
        var service = new ServiceCollection();

        _configuration = ConfigurationHelper.GetConfigurationBuilder("appsettings.Test.json");

        service.Configure<StackOverFlowOptions>(
            _configuration.GetSection("ExternalApies:StackOverFlow"));
        service.AddHttpClient();
        service.AddScoped<IStackOverFlowApiClient, StackOverFlowApiClient>();

        _provider = service.BuildServiceProvider();
    }

    [Fact]
    public async Task ShouldGetTags()
    {
        int count = _configuration
            .GetSection("ExternalApies:StackOverFlow:Data")
            .GetValue<int>("TagsCount");

        using var scope = _provider.CreateScope();
        var api = scope.ServiceProvider.GetRequiredService<IStackOverFlowApiClient>();

        var res = await api.GetTagsAsync();

        res.Length.Should().Be(count);
    }
}
