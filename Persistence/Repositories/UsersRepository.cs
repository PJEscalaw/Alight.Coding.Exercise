using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class UsersRepository : Repository<UsersEntity>, IUsersRepository
{
    private readonly AppDbContext _context;

    public UsersRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<UsersEntity?> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

        return user;
    }
}
