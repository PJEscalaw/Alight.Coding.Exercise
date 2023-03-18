using Persistence.Repositories.Interfaces;

namespace Persistence.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressesRepository AddressesRepository { get; }
        IEmploymentsRepository EmploymentsRepository { get; }
        IUsersRepository UsersRepository { get; }

        Task<int> CommitAsync();
        Task RollbackAsync();
    }
}
