using Domain.Entities;

namespace Persistence.Repositories.Interfaces;

public interface IAddressesRepository : IRepository<AddressesEntity> 
{
    Task<AddressesEntity?> GetAddressesByUserIdAsync(int userId);
}
