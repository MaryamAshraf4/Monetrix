using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetrix.Models;

namespace Monetrix.Configurations
{
    public class LoanEntityTypeConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.Property(l => l.InterestRate).HasPrecision(5, 2);

            builder.Property(l => l.Status).HasConversion<string>();

            builder.HasOne(l => l.AppUser)
                .WithMany(u => u.Loans)
                .HasForeignKey(l => l.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Customer)
                .WithMany(c => c.Loans)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
