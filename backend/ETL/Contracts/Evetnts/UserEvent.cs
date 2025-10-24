using Contracts.Dtos.StackOverFlow;

namespace Contracts.Evetnts;

public record UserEvent
{
    public UserDto[] Users { get; init; }
}
