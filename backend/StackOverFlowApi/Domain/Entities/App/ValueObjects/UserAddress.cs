using Shared.Domain;

namespace Domain.Entities.App.ValueObjects;

public class UserAddress : ValueObject<UserAddress>
{
    public UserAddress(string city, string street)
    {
        City = city;
        Street = street;
    }

    public string? City { get; init; }
    public string? Street { get; init; }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
    }
}
