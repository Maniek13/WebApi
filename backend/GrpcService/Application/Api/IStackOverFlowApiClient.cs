using Domain.Dtos;

namespace Application.Api;

public interface IStackOverFlowApiClient
{
    Task<UserDto[]> GetUsersAsync(CancellationToken ct);
    Task<UserDto?> GetUserAsync(long userId, CancellationToken ct);
}
