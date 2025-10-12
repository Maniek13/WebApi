using Contracts.Dtos;
using FluentAssertions;
using Shared.Pagination;
using System.Net.Http.Json;

namespace EndToEndTests.Endpoints;
public partial class EnpointTests : IClassFixture<WebApiWebAplicationFactory>
{
   
    [Fact]
    public async Task ShouldGetTenWhenHaveData()
    {
        var result = await _httpClient.GetAsync($"/Tags");
        result.EnsureSuccessStatusCode();

        var content = await result.Content.ReadFromJsonAsync<PagedList<TagDto>>();

        content?.Items.Should().HaveCountGreaterThan(1000);
    }
}