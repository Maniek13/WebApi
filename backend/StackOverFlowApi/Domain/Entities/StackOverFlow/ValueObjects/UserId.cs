using Shared.Domain;

namespace Domain.Entities.StackOverFlow.ValueObjects;

public class UserId : EntityId<UserId, int>
{
    public UserId(int value) : base(value)
    {
    }
}
