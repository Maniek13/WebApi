using Shared.Domain;

namespace Domain.Entities.StackOverFlow.ValueObjects;

public class QuestionId : EntityId<QuestionId, int>
{
    public QuestionId(int value) : base(value)
    {
    }

}
