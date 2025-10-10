using Abstractions.Caches;
using Abstractions.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Commands;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, Tag[]>
{
    private readonly IMemoryCache _cache;
    private readonly ICacheVersionService _cacheVersionService;
    private readonly ITagsRepositoryRO _tagsRepositoryRO;

    public GetTagsQueryHandler(IMemoryCache cache, ICacheVersionService cacheVersionService, ITagsRepositoryRO tagsRepositoryRO)
    {
        _cache = cache;
        _cacheVersionService = cacheVersionService;
        _tagsRepositoryRO = tagsRepositoryRO;
    }
    async Task<Tag[]> IRequestHandler<GetTagsQuery, Tag[]>.Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        var version = _cacheVersionService.GetVersion;
        string cacheKey = $"tags-v{version}";

        if (!_cache.TryGetValue(cacheKey, out Tag[] req))
        {
            req = _tagsRepositoryRO.GetTags().ToArray();

            _cache.Set(cacheKey, req, TimeSpan.FromMinutes(10));
        }


        return req;
    }
}
