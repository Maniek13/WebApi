using Abstractions.Caches;
using Abstractions.Repositories;
using Abstractions.Services;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Commands;

public class RefreshTagsQueryHandler : IRequestHandler<RefreshTagsQuery>
{
    private readonly ITagService _tagService;
    private readonly ITagsRepository _tagsRepository;

    public RefreshTagsQueryHandler(ITagService tagService, ITagsRepository tagsRepository)
    {
        _tagService = tagService;
        _tagsRepository = tagsRepository;
    }
    public async Task Handle(RefreshTagsQuery request, CancellationToken cancellationToken)
    {
        await _tagService.RefreshTags(cancellationToken);
    }
}
