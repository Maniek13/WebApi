namespace Domain.Entities.StackOverFlow;

public class Question  : Entity<Question>
{
    public Question(string title, string[] tags, string link, long createDateTimeStamp)
    {
        Title = title;
        Tags = tags;
        Link = link;
        CreateDateTimeStamp = createDateTimeStamp;
    }

    private Question() : base()
    {
    }

    public string Title { get; private set; }
    public string[] Tags { get; private set; }
    public string Link { get; private set; }

    public long CreateDateTimeStamp { get; private set; }

    public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreateDateTimeStamp).UtcDateTime;

    public static Question Create(string title, string[] tags, string link, long createDateTimeStamp) =>
        new Question(title, tags, link, createDateTimeStamp);

    public override int GetHashCode() => HashCode.Combine(Id, Title, Tags.Aggregate(0, (current, str) => HashCode.Combine(current, str)), Link, Create);

    public void Update(string[] tags, string link)
    {
        Tags = tags;
        Link = link;
    }

}
