using Shared.Domain;

namespace Domain.Entities.StackOverFlow.ValueObjects;

public sealed class UserNumber : SingleValueObject<long?>
{
    public UserNumber(long? value) : base(value)
    {
    }

    public static implicit operator long?(UserNumber userNumber) => userNumber.Value;
    public static explicit operator UserNumber(long? value) => new UserNumber(value);

    public static UserNumber CreateEmpty() => new UserNumber(null);
}
