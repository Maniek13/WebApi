using Abstractions.Caches;
using Abstractions.Repositories;
using Contracts.Dtos;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Commands;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, TagDto[]>
{
    private readonly IMemoryCache _cache;
    private readonly ICacheVersionService _cacheVersionService;
    private readonly ITagsRepositoryRO _tagsRepositoryRO;
    private readonly IMapper _mapper;

    public GetTagsQueryHandler(IMemoryCache cache, ICacheVersionService cacheVersionService, ITagsRepositoryRO tagsRepositoryRO, IMapper mapper)
    {
        _cache = cache;
        _cacheVersionService = cacheVersionService;
        _tagsRepositoryRO = tagsRepositoryRO;
        _mapper = mapper;
    }
    async Task<TagDto[]> IRequestHandler<GetTagsQuery, TagDto[]>.Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {

        var version = _cacheVersionService.GetVersion;
        string cacheKey = $"tags-v{version}-page-{request.Page}-pageSize-{request.PageSize}-sort-{request.SortBy}-descanding-{request.Descanding}";

        if (!_cache.TryGetValue(cacheKey, out TagDto[]? req))
        {
            req = _mapper.Map<IEnumerable<Tag>, TagDto[]>(await _tagsRepositoryRO.GetTags(request.Page, request.PageSize, request.SortBy, request.Descanding, cancellationToken));

            _cache.Set(cacheKey, req, TimeSpan.FromMinutes(10));
        }

        return req != null ? req : [];
    }
}
