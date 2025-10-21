using Application.Api;
using Domain.Dtos;
using MediatR;

namespace Application.Commands.StackOverFlow;

public class FetchDataHandler : IRequestHandler<GetUsersQuery, UserDto[]>
{
    private readonly IStackOverFlowApiClient _StackOverFlowApiClient;

    public FetchDataHandler(IMediator mediator, IStackOverFlowApiClient stackOverFlowApiClient)
    {
        _StackOverFlowApiClient = stackOverFlowApiClient;
    }

    public async Task<UserDto[]> Handle(GetUsersQuery request, CancellationToken cancellationToken) 
        => await _StackOverFlowApiClient.GetUsersAsync(cancellationToken);
}
