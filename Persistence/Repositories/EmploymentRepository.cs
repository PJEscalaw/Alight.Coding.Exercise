using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class EmploymentRepository : Repository<EmploymentsEntity>, IEmploymentsRepository
{
    private readonly AppDbContext _context;

    public EmploymentRepository(AppDbContext context) : base(context) 
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<EmploymentsEntity>> GetEmploymentsByUserId(int userId) 
        => await Task.FromResult(_context.Employments
            .Where(x => x.UserId == userId)
            .AsNoTracking()
            .AsEnumerable());
}
