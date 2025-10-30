using Shared.Domain;

namespace Domain.Entities.StackOverFlow.ValueObjects;

public sealed class QuestionNumber : SingleValueObject<long>
{
    public QuestionNumber(long value) : base(value)
    {
    }
    public static implicit operator long(QuestionNumber userNumber) => userNumber.Value;
    public static explicit operator QuestionNumber(long value) => new QuestionNumber(value);
}
