using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class UsersRepository : Repository<UsersEntity>, IUsersRepository
{
    public UsersRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
