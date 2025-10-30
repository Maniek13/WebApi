using Abstractions.DbContexts;
using Application.Interfaces.App;
using Domain.Entities.App;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DbContexts.App;
using Shared.Helpers;
using System.Net;
using Testcontainers.MsSql;

namespace IntegrationTests.Services;

public class AuthServiceTests
{
    private readonly Random _random = new Random();
    private readonly IServiceProvider _provider;
    private readonly MsSqlContainer _dbConteiner = new MsSqlBuilder()
        .Build();

    public AuthServiceTests()
    {
        Task.Run(async () => await _dbConteiner.StartAsync()).Wait();
        var service = new ServiceCollection();

        service.AddDbContext<AbstractAppDbContext, AppDbContext>(o =>
            o.UseSqlServer(_dbConteiner.GetConnectionString()));

        service.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<AbstractAppDbContext>()
          .AddDefaultTokenProviders();

        service.AddScoped<IAuthService, AuthService>();
        service.AddLogging();

        var configuration = ConfigurationHelper.GetConfigurationBuilder("appsettings.Test.json");

        service.AddSingleton(configuration);

        _provider = service.BuildServiceProvider();

        using var scope = _provider.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AbstractAppDbContext>();
        appDbContext.Database.EnsureCreated();
    }

    [Fact]
    public async Task ShouldCreateUserAsyncWithDefaultRoleWhenGetCorrectData()
    {
        var scope = _provider.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AbstractAppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        string login = _random.Next(0, 1000000).ToString(), password = "Aa123456!";

        await authService.CreateUserAsync(login, password, []);

        var user = appDbContext.Users.FirstOrDefault(el => el.UserName == login);

        user.Should().NotBe(default(ApplicationUser));
        
        (await roleManager.RoleExistsAsync("User")).Should().BeTrue();

        (await userManager.CheckPasswordAsync(user, password)).Should().BeTrue();
    }

    [Fact]
    public async Task ShouldRefreshTokenAsyncWhenGetRefreshToken()
    {
        var scope = _provider.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string login = _random.Next(0, 1000000).ToString(), password = "Aa123456!";


        var user = new ApplicationUser { UserName = login };
        _ = await userManager.CreateAsync(user, password);

        var tokenData = await authService.LoginAsync(login, password, IPAddress.None.ToString());

        await Task.Delay(1000);

        var result = await authService.RefreshTokenAsync(tokenData.refreshToken, IPAddress.None.ToString());

        result.accesToken.Should().NotBeNullOrWhiteSpace();
        result.refreshToken.Should().NotBeNullOrWhiteSpace();

        result.accesToken.Should().NotBe(tokenData.accesToken);
        result.refreshToken.Should().NotBe(tokenData.refreshToken);
    }
    [Fact]
    public async Task ShouldLoginWhenHaveCorectData()
    {
        var scope = _provider.CreateScope();
        var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string login = _random.Next(0, 1000000).ToString(), password = "Aa123456!";


        var user = new ApplicationUser { UserName = login };
        _ = await userManager.CreateAsync(user, password);

        var res = await authService.LoginAsync(login, password, IPAddress.None.ToString());

        res.accesToken.Should().NotBeNullOrWhiteSpace();
        res.refreshToken.Should().NotBeNullOrWhiteSpace();
    }
}
