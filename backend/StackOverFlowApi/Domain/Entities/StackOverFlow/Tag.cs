using Domain.Entities.StackOverFlow.EntityIds;
using Shared.Domain;

namespace Domain.Entities.StackOverFlow;

public sealed class Tag : Entity<TagId>
{
    private Tag() : base() { }

    private Tag(string name, long count, double? participation = null) : base()
    {
        Name = name;
        Count = count;
        Participation = participation;
    }

    public string Name { get; private set; }
    public long Count { get; private set; }
    public double? Participation { get; private set; }

    public List<Question>? Questions { get; private set; } = [];

    public static Tag Create(string Name, long Count, double? participation = null) =>
        new Tag(Name, Count, participation);


    public bool Equals(Tag? other) =>
        other == null ? 
            false :
            (Id == null && other.Id == null) || (Id != null && Id.Equals(other.Id)) && Name.Equals(other.Name) && Count.Equals(other.Count) && Participation.Equals(other.Participation);

    public override int GetHashCode() => HashCode.Combine(Id, Name, Count, Participation);
}
