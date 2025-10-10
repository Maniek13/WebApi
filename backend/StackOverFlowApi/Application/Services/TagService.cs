using Application.Commands;
using MediatR;
using Abstractions.Caches;

namespace Application.Services;

public class TagService
{
    private readonly ICacheVersionService _cacheVersionService;
    private readonly IMediator _mediator;
    public TagService(IMediator mediator ,ICacheVersionService cacheVersionService)
    {
        _mediator = mediator;
        _cacheVersionService = cacheVersionService;
    }
    public async Task UpdateTags()
    {
        var res = await _mediator.Send(new RefreshTagsQuery());
        if(res)
            _cacheVersionService.Invalidate();
        throw new NotImplementedException();
    }
}
