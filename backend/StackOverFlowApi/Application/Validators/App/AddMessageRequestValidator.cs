using Contracts.Requests.App;
using FluentValidation;

namespace Application.Validators;

public class AddMessageRequestValidator : AbstractValidator<AddMessageRequest>
{
    public AddMessageRequestValidator()
    {
        RuleFor(x => x.Message)
            .MaximumLength(200);
    }
}