using Domain.Entities;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class AddressesRepository : Repository<AddressesEntity>, IAddressesRepository
{
    public AddressesRepository(AppDbContext dbContext) : base(dbContext) { }
}
