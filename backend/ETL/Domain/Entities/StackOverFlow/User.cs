namespace Domain.Entities.StackOverFlow;

public class User  : Entity<User>
{
    public User(long userId, string dispalaName, long createdAt) : base()
    {
        DisplayName = dispalaName;
        CreatedAt = createdAt;
        UserId = userId;
    }

    private User() : base()
    {
    }
    public long UserId { get; private set; }
    public string DisplayName { get; private set; }
    public long CreatedAt { get; private set; }

    public ICollection<Question> Questions { get; private set; } = new List<Question>();

    public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreatedAt).UtcDateTime;

    public static User Create(long userId, string dispalaName, long createdAt) =>
        new User(userId, dispalaName, createdAt);

    public override int GetHashCode() => HashCode.Combine(Id, UserId, DisplayName, CreatedAt);

    public void Update(string dispalaName)
    {
        DisplayName = dispalaName;
    }

}
