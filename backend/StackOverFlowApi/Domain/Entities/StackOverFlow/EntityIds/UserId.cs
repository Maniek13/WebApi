using Shared.Domain;

namespace Domain.Entities.StackOverFlow.EntityIds;

public sealed class UserId : EntityId<UserId, int>
{
    public UserId(int value) : base(value)
    {
    }
}
