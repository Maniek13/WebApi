using Contracts.Dtos.StackOverFlow;

namespace Contracts.Evetnts;

public record QuestionEvent
{
    public UserDto[] Users { get; init; }
    public QuestionDto[] Questions { get; init; }
}
