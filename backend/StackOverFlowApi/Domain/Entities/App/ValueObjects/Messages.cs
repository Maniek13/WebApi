using Shared.Domain;

namespace Domain.Entities.App.ValueObjects;

public class Messages : ValueObject<Messages>
{
    public Messages(string message)
    {
        Message = message;
    }

    public string Message { get; init; }
    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Message;
    }
}
