namespace Domain.Entities.StackOverFlow;

public class User  : Entity<User>
{
    private User(long userId, string dispalaName, long createdAt) : base()
    {
        UserId = userId;
        DispalaName = dispalaName;
        CreatedAt = createdAt;
    }
    protected User()
    {
    }

    public virtual long UserId { get; protected set; }
    public virtual string DispalaName { get; protected set; }
    public virtual long CreatedAt { get; protected set; }

    public virtual DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreatedAt).UtcDateTime;

    public static User Create(long userId, string dispalaName, long createdAt) =>
        new User(userId, dispalaName, createdAt);

    public virtual new int GetHashCode() => HashCode.Combine(Id, UserId, DispalaName, CreatedAt);

    public virtual void Update(string dispalaName)
    {
        DispalaName = dispalaName;
    }
}
