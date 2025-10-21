using Contracts.Dtos.StackOverFlow;

namespace Infrastructure.Api;

public interface ISOFGrpcClient
{
    public Task<UserDto[]> GetUsersAsync(CancellationToken ct);
}
