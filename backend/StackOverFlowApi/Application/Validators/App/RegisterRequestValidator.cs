using Contracts.Requests.App;
using FluentValidation;

namespace Application.Validators.App;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Login is required");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[^a-zA-Z]").WithMessage("Password must contain at least one non-alphabetic character.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

    }
}