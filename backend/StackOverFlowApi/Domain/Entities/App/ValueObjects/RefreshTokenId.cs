using Shared.Domain;

namespace Domain.Entities.App.ValueObjects;

public class RefreshTokenId : EntityId<RefreshTokenId, int>
{
    public RefreshTokenId(int value) : base(value)
    {
    }

}
