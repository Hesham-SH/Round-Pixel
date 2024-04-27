using System.Reflection;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class ApplicationDatabaseContext : IdentityDbContext<AppUser>
{
    public ApplicationDatabaseContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<AppUser> Customers {get; set;}
    public DbSet<Item> Items {get; set;}
    public DbSet<Order> Orders {get; set;}
    public DbSet<OrderDetails> OrderDetails {get; set;}
    public DbSet<UnitOfMeasurement> UnitOfMeasurements {get; set;}
    public DbSet<Currency> Currencies {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
}
