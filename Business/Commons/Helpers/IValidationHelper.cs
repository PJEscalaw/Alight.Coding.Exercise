namespace Business.Commons.Helpers;

public interface IValidationHelper
{
    Task<bool> UserExistsAsync(int id, CancellationToken cancellationToken);
    Task<bool> EmailNotExistsAsync(string email, CancellationToken cancellationToken);
}
