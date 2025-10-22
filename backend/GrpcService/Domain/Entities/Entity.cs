using System.Reflection;

namespace Domain.Entities;

public class Entity<T> : IEquatable<T>
    where T : Entity<T>
{
    public virtual int Id { get; protected set; }

    public virtual bool Equals(T? other) => other == null ? false : Id.Equals(other.Id);

    public virtual new int GetHashCode() => HashCode.Combine(Id);

    public static bool CheckHavePropertyByName(string propertyName) =>
        typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(el => el.Name).Any(el => el.Contains(propertyName, StringComparison.InvariantCultureIgnoreCase));
}
