using Shared.Domain;

namespace Domain.Entities.StackOverFlow.EntityIds;

public sealed class QuestionId : EntityId<QuestionId, int>
{
    public QuestionId(int value) : base(value)
    {
    }

}
