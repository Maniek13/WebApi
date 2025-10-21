using Application.Commands.StackOverFlow;
using Contracts.Messages;
using Domain.Dtos;
using Grpc.Core;
using MapsterMapper;
using MediatR;

namespace Infrastructure.Services;

public class UsersService : Users.UsersBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UsersService(IMediator mediator, IMapper mapper) : base()
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public override async Task<GetUsersReply> GetUsers(GetUsersRequest request, ServerCallContext context)
    {
        var users = await _mediator.Send(new GetUsersQuery(), context.CancellationToken);


        var reply = new GetUsersReply();

        var usersResponse = _mapper.Map<UserDto[], User[]>(users);
        reply.Users.AddRange(usersResponse);

        return reply;
    }
}
