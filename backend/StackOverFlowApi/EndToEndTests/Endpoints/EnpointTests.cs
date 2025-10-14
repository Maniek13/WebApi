using EndToEndTests.ApplicationFactory;
using Microsoft.AspNetCore.TestHost;

namespace EndToEndTests.Endpoints;

public partial class EnpointTests : IClassFixture<WebApiWebAplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly TestServer _testServer;
    public EnpointTests(WebApiWebAplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _testServer = factory.Server;

    }
}