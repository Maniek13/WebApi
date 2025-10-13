using Contracts.Dtos.StackOverFlow;
using EndToEndTests.ApplicationFactory;
using FluentAssertions;
using Shared.Pagination;
using System.Net.Http.Json;
using Presentation.Routes.StackOverFlow;

namespace EndToEndTests.Endpoints;
public partial class EnpointTests : IClassFixture<WebApiWebAplicationFactory>
{
   
    [Fact]
    public async Task ShouldGetTenWhenHaveData()
    {
        var result = await _httpClient.GetAsync($"/{TagsRoutes.Get}");
        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadFromJsonAsync<PagedList<TagDto>>();

        content?.Items.Should().HaveCount(100);
    }
}