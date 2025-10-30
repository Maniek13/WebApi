namespace Shared.Interfaces;

public interface IValueObject<T> : IEquatable<T>
    where T : IValueObject<T>
{
    abstract IEnumerable<object?> GetEqualityComponents();
    abstract int GetHashCode();
    abstract string ToString();
}
