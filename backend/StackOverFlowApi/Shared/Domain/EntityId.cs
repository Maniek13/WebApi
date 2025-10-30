using Shared.Entities;

namespace Shared.Domain;

public abstract class EntityId<TSelf, TValue> : IEntityId<TSelf, TValue>
    where TSelf : EntityId<TSelf, TValue>
{
    public TValue Id { get; }

    protected EntityId(TValue value) => Id = value;

    public bool IsEmpty() => Equals(Id, default(TValue));

    public override string ToString() => Id?.ToString() ?? "";

    public bool Equals(TSelf? other) => other is not null && EqualityComparer<TValue>.Default.Equals(Id, other.Id);

    public override bool Equals(object? obj) => obj is TSelf other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(Id);

    public static bool operator ==(EntityId<TSelf, TValue>? left, EntityId<TSelf, TValue>? right)
        => Equals(left, right);

    public static bool operator !=(EntityId<TSelf, TValue>? left, EntityId<TSelf, TValue>? right)
        => !Equals(left, right);
}
