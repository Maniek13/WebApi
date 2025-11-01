using Contracts.Requests.App;
using FluentValidation;

namespace Application.Validators.App;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.ZipCode)
            .Must(el => el.IndexOf("-") == 2).WithMessage("Zipcode must be in format 00-000")
            .Matches(@"^[0-9-]+$").WithMessage("Zipcode must contain only digits and dashes.");
    }
}