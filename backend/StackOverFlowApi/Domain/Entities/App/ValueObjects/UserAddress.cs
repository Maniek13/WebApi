using Shared.Domain;

namespace Domain.Entities.App.ValueObjects;

public class UserAddress : ValueObject<UserAddress>
{
    public const int ZipCodeMaxLenght = 6;
    public UserAddress(string? city, string? street, string? zipCode)
    {
        City = city;
        Street = street;

        if(string.IsNullOrWhiteSpace(zipCode) || (zipCode.Length != ZipCodeMaxLenght && zipCode.IndexOf('-') != 2)) 
            throw new ArgumentException("Zip code must be empty or in format 00-000");
            
        ZipCode = zipCode;
    }

    public string? City { get; init; }
    public string? ZipCode { get; init; }
    public string? Street { get; init; }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return City;
        yield return Street;
        yield return ZipCode;
    }
}
