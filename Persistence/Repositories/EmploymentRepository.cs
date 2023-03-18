using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class EmploymentRepository : Repository<EmploymentsEntity>, IEmploymentsRepository
{
    public EmploymentRepository(AppDbContext dbContext) : base(dbContext) { }
}
