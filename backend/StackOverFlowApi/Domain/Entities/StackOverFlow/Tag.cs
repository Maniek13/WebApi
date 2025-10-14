using Shared.Entities;

namespace Domain.Entities.StackOverFlow;

public class Tag : Entity<Tag>
{
    private Tag() : base()
    {
    }
    private Tag(string name, long count, double? participation = null) : base()
    {
        Name = name;
        Count = count;
        Participation = participation;
    }

    public string Name { get; private set; }
    public long Count { get; private set; }
    public double? Participation { get; private set; }

    public static Tag Create(string Name, long Count, double? participation = null) =>
        new Tag(Name, Count, participation);


    new public bool Equals(Tag? other) => 
        other == null ? 
            false : 
            Id.Equals(other.Id) && Name.Equals(other.Name) && Count.Equals(other.Count) && Participation.Equals(other.Participation);

    public override int GetHashCode() => HashCode.Combine(Id, Name, Count, Participation);
}
