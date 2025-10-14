using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.Repositories;
using Application.Interfaces.StackOverFlow;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;

namespace Infrastructure.Services.DataServices;

public class StackOverFlowDataService : IStackOverFlowDataService
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly ITagsRepository _tagsRepository;
    private readonly ITagsRepositoryRO _tagsRepositoryRO;
    private readonly ICacheVersionService _cacheVersionService;
    private readonly IMapper _mapper;

    public StackOverFlowDataService(
        IStackOverFlowApiClient stackOverFlowApiClient, 
        ITagsRepository tagsRepository, 
        ITagsRepositoryRO tagsRepositoryRO, 
        ICacheVersionService cacheVersionService, 
        IMapper mapper)
    {
        _stackOverFlowApiClient = stackOverFlowApiClient;
        _tagsRepository = tagsRepository;
        _tagsRepositoryRO = tagsRepositoryRO;
        _cacheVersionService = cacheVersionService;
        _mapper = mapper;
    }

    public async Task SyncAsync(bool forceRefresh = true, CancellationToken cancellationToken = new CancellationToken())
    {
       if (!forceRefresh)
            if (await _tagsRepositoryRO.CheckHaveData(cancellationToken))
                return;

        var tagsDtos = await _stackOverFlowApiClient.GetTagsAsync();
        var recordsCount = tagsDtos.Sum(el => el.Count);

        var tags = tagsDtos.Select(el => _mapper.Map<(TagDto, long), Tag>((el, recordsCount))).ToList();

        await _tagsRepository.SetTagsAsync(tags, cancellationToken);
        _cacheVersionService.Invalidate();
    }
}
