using Shared.Domain;

namespace Domain.Entities.StackOverFlow.EntityIds;

public class TagId : EntityId<TagId, int>
{
    public TagId(int value) : base(value)
    {
    }
}
