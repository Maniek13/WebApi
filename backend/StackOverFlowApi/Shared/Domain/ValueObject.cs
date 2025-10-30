using Shared.Interfaces;

namespace Shared.Domain;

public abstract class ValueObject<T> : IValueObject<T> 
{
    public T? Value { get; }
    public ValueObject(T? value)
    {
        Value = value;
    }

    public bool Equals(T? other) => other == null ? false : Value!.Equals(other);
    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
        => Equals(left, right);
    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
        => !Equals(left, right);
    public override bool Equals(object? obj)  => obj is T other && Equals(other);

    public new string ToString() => Value?.ToString() ?? "";
    public override int GetHashCode() => HashCode.Combine(Value);
    public IEnumerable<T?> GetEqualityComponents()
    {
        yield return Value;
    }
}
