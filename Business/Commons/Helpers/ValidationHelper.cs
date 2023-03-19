using Persistence.UnitOfWork;

namespace Business.Commons.Helpers;

public class ValidationHelper : IValidationHelper
{
    private readonly IUnitOfWork _unitOfWork;

    public ValidationHelper(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> UserExistsAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersRepository.GetByIdAsync(id);

        return user != null;
    }

    public async Task<bool> EmailNotExistsAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(email);

        return user == null;
    }
}
