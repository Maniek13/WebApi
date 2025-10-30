using Shared.Interfaces;

namespace Shared.Domain;

public abstract class ValueObject<T> : IValueObject<T>
        where T : ValueObject<T>
{

    public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right) => Equals(left, right);
    public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right) => !Equals(left, right);

    public override bool Equals(object? obj)
    {
        if (obj is not T other)
            return false;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public bool Equals(T? other)
    {
        if (other is null) return false;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override string ToString() => string.Join(", ", GetEqualityComponents().Select(c => c?.ToString() ?? ""));
    public override int GetHashCode() => HashCode.Combine(GetEqualityComponents);
    public abstract IEnumerable<object?> GetEqualityComponents();

}
