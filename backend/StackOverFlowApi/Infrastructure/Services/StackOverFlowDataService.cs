using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.Repositories;
using Application.Interfaces;

namespace Infrastructure.Services;

public class StackOverFlowDataService : IStackOverFlowDataService
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly ITagsRepository _tagsRepository;
    private readonly ITagsRepositoryRO _tagsRepositoryRO;
    private readonly ICacheVersionService _cacheVersionService;

    public StackOverFlowDataService(IStackOverFlowApiClient stackOverFlowApiClient, ITagsRepository tagsRepository, ITagsRepositoryRO tagsRepositoryRO, ICacheVersionService cacheVersionService)
    {
        _stackOverFlowApiClient = stackOverFlowApiClient;
        _tagsRepository = tagsRepository;
        _tagsRepositoryRO = tagsRepositoryRO;
        _cacheVersionService = cacheVersionService;
    }

    public async Task SyncAsync(bool forceRefresh = true, CancellationToken cancellationToken = new CancellationToken())
    {
        if (!forceRefresh)
        {
        }


        for (int i = 1; i <= 10; ++i)
            await _stackOverFlowApiClient.GetAsync(i, 100);


        await _tagsRepository.SetTags(cancellationToken);
        _cacheVersionService.Invalidate();
        throw new NotImplementedException();
    }
}
