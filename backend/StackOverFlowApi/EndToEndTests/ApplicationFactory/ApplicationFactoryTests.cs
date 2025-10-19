using EndToEndTests.ApplicationFactory;
using Microsoft.AspNetCore.TestHost;

namespace EndToEndTests.Endpoints;

public partial class ApplicationFactoryTests : IClassFixture<WebApiWebAplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly WebApiWebAplicationFactory _factory;
    private readonly TestServer _testServer;
    public ApplicationFactoryTests(WebApiWebAplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _factory = factory;
        _testServer = factory.Server;
    }
}