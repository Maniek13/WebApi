using EndToEndTests.ApplicationFactory;
using FluentAssertions;
using Shared.Pagination;
using System.Net.Http.Json;
using Presentation.Routes.StackOverFlow;
using Domain.Dtos.StackOverFlow;
using Contracts.Responses;

namespace EndToEndTests.Endpoints;
public partial class ApplicationFactoryTests : IClassFixture<WebApiWebAplicationFactory>
{
   
    [Fact]
    public async Task Endpoints_ShouldGetTenWhenHaveData()
    {
        var result = await _httpClient.GetAsync($"/{TagsRoutes.Get}");
        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadFromJsonAsync<PagedList<TagResponse>>();

        content?.Items.Should().HaveCount(100);
    }
}