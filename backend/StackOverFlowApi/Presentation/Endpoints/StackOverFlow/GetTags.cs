using Application.Commands.StackOverFlow;
using Contracts.Requests.StackOverFlow;
using Contracts.Responses;
using Domain.Dtos.StackOverFlow;
using Domain.Entities.StackOverFlow;
using FastEndpoints;
using MediatR;
using Presentation.Routes.StackOverFlow;
using Shared.Pagination;
using IMapper = MapsterMapper.IMapper;

namespace Presentation.Endpoints.StackOverFlow;

internal class GetTags : Endpoint<GetTagsRequest, PagedList<TagResponse>>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetTags(IMediator mediator, IMapper mapper)
    {
        _mapper = mapper;
        _mediator = mediator;

    }

    public override void Configure()
    {
        Get(TagsRoutes.Get);
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetTagsRequest req, CancellationToken ct)
    {
        if(req.Page <= 0)
            ThrowError($"Page must be greater then 0", 400);
        if (req.PageSize <= 0)
            ThrowError($"PageSize must be greater then 0", 400);

        if (!Tag.CheckHavePropertyByName(req.SortBy))
            ThrowError($"Property {req.SortBy} doesn't exist in type Tag", 400);

        var query = new GetTagsQuery
        {
            Page = req.Page,
            PageSize = req.PageSize,
            SortBy = req.SortBy,
            Descending = req.Descending,
        };

        var res = await _mediator.Send(query, ct);

        var result = new PagedList<TagResponse>(res.PageNumber, res.PageSize, res.TotalCount, _mapper.Map<List<TagDto>, List<TagResponse>>(res.Items));

        await Send.OkAsync(result);
    }
}
