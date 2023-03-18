using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Repositories.Interfaces;

namespace Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private bool _isDisposed;
    private readonly AppDbContext _dbContext;

    public IAddressesRepository AddressesRepository => new AddressesRepository(_dbContext);
    public IEmploymentsRepository EmploymentsRepository => new EmploymentRepository(_dbContext);
    public IUsersRepository UsersRepository => new UsersRepository(_dbContext);

    public UnitOfWork(AppDbContext dbContext) => _dbContext = dbContext;
    public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();
    public async Task RollbackAsync() => await _dbContext.DisposeAsync();
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;
        if (disposing) _dbContext.Dispose();

        _isDisposed = true;
    }

    ~UnitOfWork() => Dispose(false);
}
