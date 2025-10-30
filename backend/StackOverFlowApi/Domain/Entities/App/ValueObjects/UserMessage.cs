using Shared.Domain;

namespace Domain.Entities.App.ValueObjects;

public class UserMessage : ValueObject<UserMessage>
{
    public const int MaxMessageLenght = 200;

    private UserMessage()
    {
    }

    public UserMessage(string message)
    {
        if (message.Length > MaxMessageLenght)
            throw new ArgumentException("Message lenght must be less or equel 200");

        Message = message;
    }

    public string Message { get; init; }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Message;
    }
}
