using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.Interfaces;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using FluentAssertions;
using Infrastructure.Services.DataServices;
using MapsterMapper;
using Moq;

namespace UnitTests.Services.DataServices;

public class StackOverFlowDataServiceTests
{
    [Fact]
    public async Task ShouldSyncWhenHaveDataAsync()
    {

        var mockedIStackOverFlowApiClient = new Mock<IStackOverFlowApiClient>();
        mockedIStackOverFlowApiClient
            .Setup(r => r.GetTagsAsync())
            .ReturnsAsync(Array.Empty<TagDto>);

        var mockedTagsRepository = new Mock<ITagsRepository>();
        mockedTagsRepository
            .Setup(s => s.SetTagsAsync(new List<Tag>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var mockedTagsRepositoryRO = new Mock<ITagsRepositoryRO>();
        mockedTagsRepositoryRO
            .Setup(s => s.CheckHaveData(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var mockedCacheVersionService = new Mock<ICacheVersionService>();
        mockedCacheVersionService
            .Setup(p => p.Invalidate());

        var mockedMapper = new Mock<IMapper>();

        var dataService = new StackOverFlowDataService(
                mockedIStackOverFlowApiClient.Object,
                mockedTagsRepository.Object,
                mockedTagsRepositoryRO.Object,
                mockedCacheVersionService.Object,
                mockedMapper.Object
            );

        Func<Task> startAsync = () => dataService.SyncAsync(true, CancellationToken.None);
        await startAsync
            .Should()
            .NotThrowAsync();
    }
    [Fact]
    public async Task ShouldNotSyncWhenHaveDataAsync()
    {

        var mockedIStackOverFlowApiClient = new Mock<IStackOverFlowApiClient>();
        mockedIStackOverFlowApiClient
            .Setup(r => r.GetTagsAsync())
            .ReturnsAsync(Array.Empty<TagDto>);

        var mockedTagsRepository = new Mock<ITagsRepository>();
        mockedTagsRepository
            .Setup(s => s.SetTagsAsync(new List<Tag>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var mockedTagsRepositoryRO = new Mock<ITagsRepositoryRO>();
        mockedTagsRepositoryRO
            .Setup(s => s.CheckHaveData(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var mockedCacheVersionService = new Mock<ICacheVersionService>();
        mockedCacheVersionService
            .Setup(p => p.Invalidate());

        var mockedMapper = new Mock<IMapper>();

        var dataService = new StackOverFlowDataService(
                mockedIStackOverFlowApiClient.Object,
                mockedTagsRepository.Object,
                mockedTagsRepositoryRO.Object,
                mockedCacheVersionService.Object,
                mockedMapper.Object
            );

      

        Func<Task> syncAsync = () => dataService.SyncAsync(true, CancellationToken.None);
        await syncAsync
            .Should()
            .NotThrowAsync();
    }
}
