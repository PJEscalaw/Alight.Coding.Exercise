using Domain.Entities;

namespace Persistence.Repositories.Interfaces;

public interface IEmploymentsRepository : IRepository<EmploymentsEntity> 
{
    Task<EmploymentsEntity?> GetEmploymentsByIdAndUserIdAsync(int id, int userId);
    Task<IEnumerable<EmploymentsEntity?>> GetEmploymentsByUserIdAsync(int userId);
}
