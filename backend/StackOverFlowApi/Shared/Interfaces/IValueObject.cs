namespace Shared.Interfaces;

public interface IValueObject<T> : IEquatable<T>
{
    IEnumerable<T?> GetEqualityComponents();
    T? Value { get; }
    int GetHashCode();
    string ToString();
}
