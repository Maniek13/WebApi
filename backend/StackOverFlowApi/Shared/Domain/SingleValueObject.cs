using Shared.Interfaces;

namespace Shared.Domain;

public abstract class SingleValueObject<T> : ISingleValueObject<T> 
{
    public T? Value { get; }
    public SingleValueObject(T? value)
    {
        Value = value;
    }

    public bool Equals(T? other) => other == null ? false : Value!.Equals(other);
    public static bool operator ==(SingleValueObject<T>? left, SingleValueObject<T>? right)
        => Equals(left, right);
    public static bool operator !=(SingleValueObject<T>? left, SingleValueObject<T>? right)
        => !Equals(left, right);
    public override bool Equals(object? obj)  => obj == null ? false : obj.GetType() != GetType() ? false :  GetEqualityComponents().SequenceEqual(((SingleValueObject<T>)obj).GetEqualityComponents());

    public new string ToString() => Value?.ToString() ?? "";
    public override int GetHashCode() => HashCode.Combine(Value);
    public IEnumerable<T?> GetEqualityComponents()
    {
        yield return Value;
    }
}
