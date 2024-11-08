using Microsoft.EntityFrameworkCore;
using TaxiCompany.Domain.Entities;

namespace TaxiCompany.Domain.Context;

public class TaxiCompanyDbContext(DbContextOptions<TaxiCompanyDbContext> options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clinets { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Driver>()
            .HasOne<Car>()
            .WithOne()
            .HasForeignKey<Driver>(d => d.AssignedCarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Trip>()
            .HasOne<Car>()
            .WithMany()
            .HasForeignKey(t => t.AssignedCarId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Trip>()
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(t => t.AssignedClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Car>()
            .HasIndex(c => c.SerialNumber)
            .IsUnique();

        modelBuilder.Entity<Driver>()
            .HasIndex(d => d.Passport)
            .IsUnique();
    }
}

