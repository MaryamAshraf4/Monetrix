using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Monetrix.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        DbSet<Loan> Loans { get; set; } = null!;
        DbSet<Account> Accounts { get; set; } = null!;
        DbSet<AppUser> AppUsers { get; set; } = null!;
        DbSet<Customer> Customers { get; set; } = null!;
        DbSet<Transaction> Transactions { get; set; } = null!;
    }
 }
