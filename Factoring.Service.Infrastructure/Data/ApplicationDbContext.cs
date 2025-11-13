using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Factoring.Service.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Example of Fluent API configuration
        modelBuilder.Entity<Invoice>()
            .Property(i => i.Amount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Invoices)
            // if you don't want to specify the navigation property on Invoice
            .WithOne() 
            // if you want to specify the navigation property on Invoice
            // .WithOne(i => i.Customer)
            .HasForeignKey(i => i.CustomerId);
    }
}