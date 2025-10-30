using Shared.Domain;

namespace Domain.Entities.StackOverFlow.ValueObjects;

public class UserNumber : ValueObject<long>
{
    public UserNumber(long value) : base(value)
    {
    }

    public static implicit operator long(UserNumber userNumber) => userNumber.Value;
    public static explicit operator UserNumber(long value) => new UserNumber(value);
}
