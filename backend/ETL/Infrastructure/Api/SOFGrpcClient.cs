using Contracts.Dtos.StackOverFlow;
using Contracts.Messages;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Api;

public class SOFGrpcClient : ISOFGrpcClient
{
    private readonly IConfiguration _configuration;
    public SOFGrpcClient(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<UserDto[]> GetUsersAsync(CancellationToken ct)
    {
        var grpcUrl = _configuration["GrpcServices:UsersServiceUrl"]!;

        using var channel = GrpcChannel.ForAddress(grpcUrl, new GrpcChannelOptions
        {
            HttpHandler = new HttpClientHandler()
        });

        var client = new Users.UsersClient(channel);

        return (await client.GetUsersAsync(new GetUsersRequest())).Users.Select(el => new UserDto(el.UserId, el.DisplayName, el.CreationDate)).ToArray();
    }

    public async Task<UserDto> GetUserAsync(long userId, CancellationToken ct)
    {
        var grpcUrl = _configuration["GrpcServices:UsersServiceUrl"]!;

        using var channel = GrpcChannel.ForAddress(grpcUrl, new GrpcChannelOptions
        {
            HttpHandler = new HttpClientHandler()
        });

        var client = new Users.UsersClient(channel);

        var req = new GetUserRequest()
        {
            UserId = userId,
        };

        var user = (await client.GetUserAsync(req)).User;

        return new UserDto(user.UserId, user.DisplayName, user.CreationDate);
    }
}
