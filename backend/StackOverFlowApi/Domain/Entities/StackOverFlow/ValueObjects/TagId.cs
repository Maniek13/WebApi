using Shared.Domain;

namespace Domain.Entities.StackOverFlow.ValueObjects;

public class TagId : EntityId<TagId, int>
{
    public TagId(int value) : base(value)
    {
    }
}
