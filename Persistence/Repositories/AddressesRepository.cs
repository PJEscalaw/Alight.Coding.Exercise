using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class AddressesRepository : Repository<AddressesEntity>, IAddressesRepository
{
    private readonly AppDbContext _context;

    public AddressesRepository(AppDbContext context) : base(context) 
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<AddressesEntity?> GetAddressesByUserIdAsync(int userId)
    {
        return await _context.Addresses
                            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}
