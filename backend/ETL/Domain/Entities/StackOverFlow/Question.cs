namespace Domain.Entities.StackOverFlow;

public class Question  : Entity<Question>
{
    public Question(long questionId, long? userId, string title, string[] tags, string link, long createDateTimeStamp) : base()
    {
        QuestionId = questionId;
        Title = title;
        Tags = tags;
        Link = link;
        CreateDateTimeStamp = createDateTimeStamp;
        UserId = userId;
    }

    private Question() : base()
    {
    }

    public long QuestionId { get; private set; }
    public string Title { get; private set; }
    public string[] Tags { get; private set; }
    public string Link { get; private set; }
    public long CreateDateTimeStamp { get; private set; }
    public long? UserId { get; private set; }
    public User? User { get; private set; }
    public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreateDateTimeStamp).UtcDateTime;

    public static Question Create(long questionId, long? userId, string title, string[] tags, string link, long createDateTimeStamp) =>
        new Question(questionId, userId, title, tags, link, createDateTimeStamp);

    public override int GetHashCode() => HashCode.Combine(Id, QuestionId, UserId, Title, Tags.Aggregate(0, (current, str) => HashCode.Combine(current, str)), Link, CreateDateTimeStamp);

    public void Update(string[] tags, string link, string title)
    {
        Title = title;
        Tags = tags;
        Link = link;
    }
}
