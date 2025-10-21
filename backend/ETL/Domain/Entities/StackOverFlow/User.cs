namespace Domain.Entities.StackOverFlow;

public class User  : Entity<User>
{
    public User(long accountId, string dispalaName, long createdAt)
    {
        AccountId = accountId;
        DispalaName = dispalaName;
        CreatedAt = createdAt;
    }

    private User() : base()
    {
    }

    public long AccountId { get; private set; }
    public string DispalaName { get; private set; }
    public long CreatedAt { get; private set; }

    public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreatedAt).UtcDateTime;

    public static User Create(long accountId, string dispalaName, long createdAt) =>
        new User(accountId, dispalaName, createdAt);

    public override int GetHashCode() => HashCode.Combine(Id, AccountId, DispalaName, CreatedAt);

    public void Update(string dispalaName)
    {
        DispalaName = dispalaName;
    }

}
