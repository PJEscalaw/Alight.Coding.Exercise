using Business.Commons.Helpers;
using FluentValidation;

namespace Business.Features.Users.Queries;

public class GetUsersByIdQueryValidator : AbstractValidator<GetUsersByIdQuery>
{
    private readonly IValidationHelper _validationHelper;

    public GetUsersByIdQueryValidator(IValidationHelper validationHelper)
    {
        _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        
        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id must not be empty.")
            .NotEqual(0)
            .WithMessage("Id must not be equals zero.")
            .MustAsync(_validationHelper.UserExistsAsync)
            .WithMessage("User id {PropertyValue} not found.");
    }
}
