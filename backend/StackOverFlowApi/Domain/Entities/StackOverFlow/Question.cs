using Domain.Entities.StackOverFlow.ValueObjects;
using Shared.Domain;

namespace Domain.Entities.StackOverFlow;

public class Question : Entity<QuestionId>
{
    private Question() : base() { }
    public Question(QuestionNumber questionNumber, UserNumber userNumber, string title, string[] tags, string link, long createDateTimeStamp) : base()
    {
        QuestionNumber = questionNumber;
        Title = title;
        Tags = tags;
        Link = link;
        CreateDateTimeStamp = createDateTimeStamp;
        UserNumber = userNumber;
    }

    public QuestionNumber QuestionNumber { get; private set; }
    public string Title { get; private set; }
    public string[] Tags { get; private set; }
    public string Link { get; private set; }
    public UserNumber UserNumber { get; private set; }

    public User? User { get; private set; }

    public long CreateDateTimeStamp { get; private set; }

    public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreateDateTimeStamp).UtcDateTime;

    public static Question Create(QuestionNumber questionNumber, UserNumber userNumber, string title, string[] tags, string link, long createDateTimeStamp) =>
        new Question(questionNumber, userNumber, title, tags, link, createDateTimeStamp);

    public override int GetHashCode() => HashCode.Combine(Id, QuestionNumber, UserNumber, Title, Tags.Aggregate(0, (current, str) => HashCode.Combine(current, str)), Link, CreateDateTimeStamp);
    public void Update(string[] tags, string link, string title)
    {
        Title = title;
        Tags = tags;
        Link = link;
    }
}
