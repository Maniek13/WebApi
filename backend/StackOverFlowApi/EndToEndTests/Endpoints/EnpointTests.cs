namespace EndToEndTests.Endpoints;

public partial class EnpointTests : IClassFixture<WebApiWebAplicationFactory>
{
    private readonly HttpClient _httpClient;
    public EnpointTests(WebApiWebAplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }
}