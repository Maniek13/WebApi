using Abstractions.Caches;
using Abstractions.Services;

namespace Application.Services;

public class TagService : ITagService
{
    private readonly ICacheVersionService _cacheVersionService;
    public TagService(ICacheVersionService cacheVersionService)
    {
        _cacheVersionService = cacheVersionService;
    }
    public async Task RefreshTags(CancellationToken ct)
    {
        throw new NotImplementedException();
        _cacheVersionService.Invalidate();
    }

    public async Task SetTags(CancellationToken ct)
    {
        throw new NotImplementedException();
        _cacheVersionService.Invalidate();
    }
}
