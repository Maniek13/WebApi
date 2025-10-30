namespace Shared.Entities
{
    public interface IEntityId<TSelf, TValue> : IEquatable<TSelf>
        where TSelf : IEntityId<TSelf, TValue>
    {
        TValue Id { get; }
    }
}
