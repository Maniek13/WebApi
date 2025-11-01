using Contracts.Requests.StackOverFlow;
using Domain.Entities.StackOverFlow;
using FluentValidation;

namespace Application.Validators.SOF;

public class GetTagsRequestValidator : AbstractValidator<GetTagsRequest>
{
    public GetTagsRequestValidator()
    {
        RuleFor(x => x.SortBy)
            .Must(el => !Tag.CheckHavePropertyByName<Tag>(el))
            .WithMessage(el => $"Property {el} doesn't exist in type Tag");

        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page must be greater then 0");
        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater then 0");
    }
}