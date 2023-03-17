using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

/// <summary>
/// DbContext of the app.
/// </summary>
public class AppDbContext : DbContext
{
    public DbSet<EmploymentsEntity> Employments { get; set; }
    public DbSet<AddressesEntity> Addresses { get; set; }
    public DbSet<UsersEntity> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
}
