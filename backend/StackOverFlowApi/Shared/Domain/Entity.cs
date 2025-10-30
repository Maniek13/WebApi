using System.Reflection;

namespace Shared.Domain;

public abstract class Entity<TId> : IEquatable<TId>
    where TId : notnull
{
    public TId Id { get; init; }

    protected Entity() { }

    protected Entity(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) => Equals(left, right);
    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) => !Equals(left, right);
    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id.Equals(other.Id);
    }
    public bool Equals(TId? other) => other == null ? false : Id.Equals(other);

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool CheckHavePropertyByName<TEntity>(string propertyName)
        where TEntity : Entity<TId> =>
        typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(el => el.Name).Any(el => el.Contains(propertyName, StringComparison.InvariantCultureIgnoreCase));
}
