using Domain.Entities;

namespace Persistence.Repositories.Interfaces;

public interface IUsersRepository : IRepository<UsersEntity> 
{
    Task<UsersEntity?> GetUserByEmailAsync(string email);
}