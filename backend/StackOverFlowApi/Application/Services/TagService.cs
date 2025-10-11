using Abstractions.Caches;
using Abstractions.Repositories;
using Abstractions.Services;

namespace Application.Services;

public class TagService : ITagService
{
    private readonly ITagsRepository _tagsRepository;
    private readonly ICacheVersionService _cacheVersionService;
    public TagService(ICacheVersionService cacheVersionService, ITagsRepository tagsRepository)
    {
        _tagsRepository = tagsRepository;
        _cacheVersionService = cacheVersionService;
    }
    public async Task RefreshTags(CancellationToken ct)
    {
        throw new NotImplementedException();
        await _tagsRepository.RefreshTags(ct);
        _cacheVersionService.Invalidate();
    }

    public async Task SetTags(CancellationToken ct)
    {
        throw new NotImplementedException();
        await _tagsRepository.SetTags(ct);
        _cacheVersionService.Invalidate();
    }
}
