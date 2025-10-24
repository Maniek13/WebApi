using Contracts.Dtos.StackOverFlow;

namespace Infrastructure.Api;

public interface ISOFGrpcClient
{
    public Task<UserDto[]> GetUsersAsync(CancellationToken ct);
    public Task<UserDto> GetUserAsync(long userId, CancellationToken ct);
    public Task<UserDto[]> GetUsersByIdsAsync(long[] userIds, CancellationToken ct);
}
