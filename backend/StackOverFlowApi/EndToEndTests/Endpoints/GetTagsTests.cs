using Domain.Dtos.StackOverFlow;
using EndToEndTests.ApplicationFactory;
using FluentAssertions;
using Presentation.Routes.StackOverFlow;
using Shared.Pagination;
using System.Net.Http.Json;

namespace EndToEndTests.Endpoints;
public partial class ApplicationFactoryTests : IClassFixture<WebApiWebAplicationFactory>
{
   
    [Fact]
    public async Task Endpoints_ShouldGetTenWhenHaveData()
    {
        var result = await _httpClient.GetAsync($"/{TagsRoutes.Get}");
        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadFromJsonAsync<PagedList<TagDto>>();

        content?.Items.Should().HaveCount(100);
    }
}