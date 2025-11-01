using Contracts.Requests.App;
using FluentValidation;

namespace Application.Validators.App;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Login is required");
    }
}