using Shared.Domain;

namespace Domain.Entities.App.EntityIds;

public class RefreshTokenId : EntityId<RefreshTokenId, int>
{
    public RefreshTokenId(int value) : base(value)
    {
    }

}
