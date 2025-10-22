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

    public override async Task<GetUsersReply> GetUsers(GetUsersRequest req, ServerCallContext context)
    {
        var users = await _mediator.Send(new FetchUsersQuery(), context.CancellationToken);


        var reply = new GetUsersReply();

        var usersResponse = _mapper.Map<UserDto[], User[]>(users);
        reply.Users.AddRange(usersResponse);

        return reply;
    }

    public override async Task<GetUserReply> GetUser(GetUserRequest req, ServerCallContext context)
    {
        var user = await _mediator.Send(new GetUserQuery { UserId = req.UserId }, context.CancellationToken);

        return user == null ? 
            new GetUserReply() { User = null }
            : 
            new GetUserReply()
            {
                User = _mapper.Map<UserDto, User>(user)
            };
    }
}
