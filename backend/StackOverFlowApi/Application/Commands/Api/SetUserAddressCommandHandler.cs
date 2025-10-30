using Abstractions.Repositories.Api;
using Domain.Entities.App.ValueObjects;
using MediatR;

namespace Application.Commands.Api;

public class SetUserAddressCommandHandler : IRequestHandler<SetUserAddressCommand>
{
    private readonly IUserRepository _userRepository;

    public SetUserAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(SetUserAddressCommand request, CancellationToken cancellationToken)
    {
        var address = new UserAddress(request.City, request.Street, request.ZipCode);

        await _userRepository.SetUserAddress(request.UserId, address, cancellationToken);
    }
}
