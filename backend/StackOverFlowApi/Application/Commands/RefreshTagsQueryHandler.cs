using Application.Interfaces;
using MediatR;

namespace Application.Commands;

public class RefreshTagsQueryHandler : IRequestHandler<RefreshTagsQuery>
{
    private readonly IStackOverFlowDataService _tagService;

    public RefreshTagsQueryHandler(IStackOverFlowDataService tagService)
    {
        _tagService = tagService;
    }
    public async Task Handle(RefreshTagsQuery request, CancellationToken cancellationToken)
    {
        await _tagService.SyncAsync(true, cancellationToken);
    }
}
