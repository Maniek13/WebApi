namespace Shared.Interfaces;

public interface ISingleValueObject<T> : IEquatable<T>
{
    IEnumerable<T?> GetEqualityComponents();
    T? Value { get; }
    int GetHashCode();
    string ToString();
}
