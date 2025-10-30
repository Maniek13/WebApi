using Abstractions.Repositories.Api;
using Domain.Entities.App.ValueObjects;
using MediatR;

namespace Application.Commands.Api;

public class MessageCommandHandler : IRequestHandler<MessageCommand>
{
    private readonly IUserRepository _userRepository;

    public MessageCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(MessageCommand request, CancellationToken cancellationToken)
    {
        var message = new UserMessage(request.Message);

        await _userRepository.AddMessage(request.UserId, message, cancellationToken);
    }
}
