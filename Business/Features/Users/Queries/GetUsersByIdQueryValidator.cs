using FluentValidation;
using Persistence.UnitOfWork;
using SQLitePCL;

namespace Business.Features.Users.Queries;

public class GetUsersByIdQueryValidator : AbstractValidator<GetUsersByIdQuery>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersByIdQueryValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        _ = RuleFor(x => x.Id)
            .NotEmpty()
            .NotNull()
            .WithMessage("Id must not be empty.")
            .NotEqual(0)
            .WithMessage("Id must not be equals zero.")
            .MustAsync(ExistsAsync)
            .WithMessage("User id {PropertyValue} not found.");
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersRepository.GetByIdAsync(id);

        return user != null;
    }
}
