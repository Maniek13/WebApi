using Application.Interfaces;
using FluentAssertions;
using Infrastructure.HostedServices;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ServiceTests.Services;

public class StartupSyncHostedServiceTests
{
    [Fact]
    public async Task ShouldStartAsync()
    {
        var mockedDataService = new Mock<IStackOverFlowDataService>();
        mockedDataService
            .Setup(r => r.SyncAsync(false, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var mockScope = new Mock<IServiceScope>();
        mockScope
            .Setup(s => s.ServiceProvider)
            .Returns(new ServiceCollection()
            .AddSingleton(mockedDataService.Object)
            .BuildServiceProvider());

        var mockedFactory = new Mock<IServiceScopeFactory>();
        mockedFactory
            .Setup(p => p.CreateScope())
            .Returns(mockScope.Object);

        var mockedProvider = new Mock<IServiceProvider>();
        mockedProvider
            .Setup(p => p.GetService(typeof(IServiceScopeFactory)))
            .Returns(mockedFactory.Object);

        var hostedServide = new StartupSyncHostedService(mockedProvider.Object);

        Func<Task> startAsync = () => hostedServide.StartAsync(CancellationToken.None);
        await startAsync
            .Should()
            .NotThrowAsync();

        mockedDataService.VerifyAll();
    }
}
