using Domain.Common;

namespace Domain.Entities;

public class Tag : Entity
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
}
