using FluentValidation;
using Persistence.UnitOfWork;

namespace Business.Features.Users.Commands;

public class CreateUsersCommandValidator : AbstractValidator<CreateUsersCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUsersCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        _ = RuleFor(x => x.UsersInputDto.FirstName)
            .NotNull()
            .NotEmpty();

        _ = RuleFor(x => x.UsersInputDto.LastName)
            .NotNull()
            .NotEmpty();

        _ = RuleFor(x => x.UsersInputDto.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        _ = RuleFor(x => x.UsersInputDto.Address)
            .NotNull()
            .NotEmpty();

        _ = RuleFor(x => x.UsersInputDto.Employments)
           .NotNull()
           .NotEmpty();

        _ = RuleFor(x => x.UsersInputDto.Email)
           .MustAsync(NotExistsAsync)
           .WithMessage("Email '{PropertyValue}' already used.");

        ValidateAddress();
        ValidateEmployments();
    }

    private void ValidateEmployments()
    {
        _ = RuleForEach(x => x.UsersInputDto.Employments)
                .ChildRules(employment =>
                {
                    _ = employment.RuleFor(x => x.Company)
                            .NotNull()
                            .NotEmpty();

                    _ = employment.RuleFor(x => x.MonthsOfExperience)
                            .NotEmpty()
                            .NotNull()
                            .NotEqual(0);

                    _ = employment.RuleFor(x => x.Salary)
                            .NotEmpty()
                            .NotNull()
                            .NotEqual(0);

                    _ = employment.RuleFor(x => x.StartDate)
                            .NotEmpty()
                            .NotNull();

                    _ = employment.RuleFor(x => x.EndDate.Value.Date)
                            .GreaterThan(x => x.StartDate.Value.Date)
                            .WithMessage("End Date {PropertyValue} must be greater than Start Date.");
                });
    }

    private void ValidateAddress()
    {
        _ = RuleFor(x => x.UsersInputDto.Address.Street)
            .NotNull()
            .NotEmpty();

        _ = RuleFor(x => x.UsersInputDto.Address.City)
            .NotNull()
            .NotEmpty();
    }
    private async Task<bool> NotExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email);

        return user == null;
    }
}
