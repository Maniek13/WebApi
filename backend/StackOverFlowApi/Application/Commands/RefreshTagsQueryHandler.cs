using Abstractions.Services;
using MediatR;

namespace Application.Commands;

public class RefreshTagsQueryHandler : IRequestHandler<RefreshTagsQuery>
{
    private readonly ITagService _tagService;

    public RefreshTagsQueryHandler(ITagService tagService)
    {
        _tagService = tagService;
    }
    public async Task Handle(RefreshTagsQuery request, CancellationToken cancellationToken)
    {
        await _tagService.RefreshTags(cancellationToken);
    }
}
