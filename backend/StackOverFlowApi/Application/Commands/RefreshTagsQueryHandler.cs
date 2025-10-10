using MediatR;

namespace Application.Commands;

public class RefreshTagsQueryHandler : IRequestHandler<RefreshTagsQuery, bool>
{
    async Task<bool> IRequestHandler<RefreshTagsQuery, bool>.Handle(RefreshTagsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
