using Shared.Entities;

namespace Domain.Entities;

public class Tag : Entity<Tag>
{
    private Tag()
    {
    }

    private Tag(string name, long count)
    {
        Name = name;
        Count = count;
    }

    public string Name { get; private set; }
    public long Count { get; private set; }

    public static Tag Create(string Name, long Count) =>
        new Tag(Name, Count);


    new public bool Equals(Tag? other) => 
        other == null ? 
            false : 
            Id.Equals(other.Id) && Name.Equals(other.Name) && Count.Equals(other.Count);

    public override int GetHashCode() => HashCode.Combine(Id, Name, Count);
}
