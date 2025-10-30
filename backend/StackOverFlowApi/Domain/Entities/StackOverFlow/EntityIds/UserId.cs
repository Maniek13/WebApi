using Shared.Domain;

namespace Domain.Entities.StackOverFlow.EntityIds;

public class UserId : EntityId<UserId, int>
{
    public UserId(int value) : base(value)
    {
    }
}
