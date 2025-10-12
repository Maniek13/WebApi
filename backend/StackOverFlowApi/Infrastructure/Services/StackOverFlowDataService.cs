using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.Interfaces;
using Application.Interfaces;
using Contracts.Dtos;
using Domain.Entities;
using MapsterMapper;

namespace Infrastructure.Services;

public class StackOverFlowDataService : IStackOverFlowDataService
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly ITagsRepository _tagsRepository;
    private readonly ITagsRepositoryRO _tagsRepositoryRO;
    private readonly ICacheVersionService _cacheVersionService;
    private readonly IMapper _mapper;

    public StackOverFlowDataService(IStackOverFlowApiClient stackOverFlowApiClient, ITagsRepository tagsRepository, ITagsRepositoryRO tagsRepositoryRO, ICacheVersionService cacheVersionService, IMapper mapper)
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

        List<Tag> tags = [];

        for (int i = 1; i <= 10; ++i)
            tags.AddRange(_mapper.Map<TagDto[], Tag[]>(await _stackOverFlowApiClient.GetTagsAsync()));


        await _tagsRepository.SetTags(tags, cancellationToken);
        _cacheVersionService.Invalidate();
    }
}
