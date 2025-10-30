using Domain.Entities.StackOverFlow.EntityIds;
using Domain.Entities.StackOverFlow.ValueObjects;
using Shared.Domain;

namespace Domain.Entities.StackOverFlow;

public sealed class User  : Entity<UserId>
{
    private User() : base()
    {
    }

    private User(UserNumber userNumber, string dispalaName, long createdAt) : base()
    {
        UserNumber = userNumber;
        DisplayName = dispalaName;
        CreatedAt = createdAt;
    }

    public UserNumber UserNumber { get; private set; }
    public string DisplayName  { get; private set; }
    public long CreatedAt { get; private set; }
    public ICollection<Question> Questions { get; private set; } = new List<Question>();
    public DateTime CreatedDate => DateTimeOffset.FromUnixTimeMilliseconds(CreatedAt).UtcDateTime;
    public static User Create(UserNumber userNumber, string dispalaName, long createdAt) =>
        new User(userNumber, dispalaName, createdAt);

    public override int GetHashCode() => HashCode.Combine(Id, UserNumber, DisplayName, CreatedAt);

    public void Update(string dispalaName)
    {
        DisplayName = dispalaName;
    }

}
