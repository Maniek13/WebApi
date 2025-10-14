using Abstractions.Caches;
using Abstractions.Repositories;
using Contracts.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Shared.Pagination;

namespace Application.Commands.StackOverFlow;

public class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, PagedList<TagDto>>
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
    async Task<PagedList<TagDto>> IRequestHandler<GetTagsQuery, PagedList<TagDto>>.Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {

        var version = _cacheVersionService.GetVersion;
        string cacheKey = $"tags-v{version}-page-{request.Page}-pageSize-{request.PageSize}-sort-{request.SortBy}-descanding-{request.Descending}";

        if (!_cache.TryGetValue(cacheKey, out PagedList<TagDto>? response))
        {
            var list = await _tagsRepositoryRO.GetTagsAsync(request.Page, request.PageSize, request.SortBy, request.Descending, cancellationToken);
            var items = _mapper.Map<List<Tag>, List<TagDto>>(list.Items);

            response = new PagedList<TagDto>(request.Page, request.PageSize, list.TotalCount, items);

            _cache.Set(cacheKey, response, TimeSpan.FromMinutes(10));
        }


        return response != null ? response : new PagedList<TagDto>(0, 0, 0, []);
    }
}
