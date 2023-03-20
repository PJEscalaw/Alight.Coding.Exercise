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
            .NotEqual(0)
            .MustAsync(_validationHelper.UserExistsAsync)
            .WithMessage("User id {PropertyValue} not found.");
    }
}
