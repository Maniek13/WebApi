namespace Shared.Entities;

public class Entity<T> : IEquatable<T>
    where T : Entity<T>
{
    public int Id { get; }

    public bool Equals(T? other) => other == null ? false : Id.Equals(other.Id);

    public override int GetHashCode() => HashCode.Combine(Id);
}
