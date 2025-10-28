using Abstractions.Api;
using Abstractions.Caches;
using Abstractions.DbContexts;
using Abstractions.Interfaces;
using Abstractions.Repositories;
using Application.Interfaces.StackOverFlow;
using Domain.Dtos.StackOverFlow;
using MapsterMapper;
using Tag = Domain.Entities.StackOverFlow.Tag;

namespace Infrastructure.Services.DataServices;

public class StackOverFlowDataService : IStackOverFlowDataService
{
    private readonly IStackOverFlowApiClient _stackOverFlowApiClient;
    private readonly ITagsRepository _tagsRepository;
    private readonly ITagsRepositoryRO _tagsRepositoryRO;
    private readonly ICacheVersionService _cacheVersionService;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork<AbstractSOFDbContext> _unitOfWork;

    public StackOverFlowDataService(
        IStackOverFlowApiClient stackOverFlowApiClient, 
        ITagsRepository tagsRepository, 
        ITagsRepositoryRO tagsRepositoryRO, 
        ICacheVersionService cacheVersionService, 
        IMapper mapper,
        IUnitOfWork<AbstractSOFDbContext> unitOfWork)
    {
        _stackOverFlowApiClient = stackOverFlowApiClient;
        _tagsRepository = tagsRepository;
        _tagsRepositoryRO = tagsRepositoryRO;
        _cacheVersionService = cacheVersionService;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _cacheVersionService.Invalidate();
    }
}
