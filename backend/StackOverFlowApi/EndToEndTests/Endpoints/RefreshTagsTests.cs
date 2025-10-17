using Application.Interfaces.App;
using Domain.Entities.App;
using EndToEndTests.ApplicationFactory;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Routes.StackOverFlow;
using System.Net;
using System.Net.Http.Headers;

namespace EndToEndTests.Endpoints;

public partial class ApplicationFactoryTests : IClassFixture<WebApiWebAplicationFactory>
{
    private Random _random = new Random();

    [Fact]
    public async Task Endpoints_ShouldRefreshTags()
    {
        var authService = _factory.Services.GetRequiredService<IAuthService>();
        var userManager = _factory.Services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = _factory.Services.GetRequiredService<RoleManager<IdentityRole>>();

        string login = _random.Next(0, 1000000).ToString(), password = "Aa123456!";

        var user = new ApplicationUser { UserName = login };
        _ = await userManager.CreateAsync(user, password);

        if (!await roleManager.RoleExistsAsync("User"))
            await roleManager.CreateAsync(new IdentityRole("User"));

        await userManager.AddToRoleAsync(user, "User");

        var res = await authService.LoginAsync(login, password, IPAddress.None.ToString());

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", res.accesToken);

        var result = await _httpClient.PutAsync($"/{TagsRoutes.Refresh}", null);
        result.EnsureSuccessStatusCode();

        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}