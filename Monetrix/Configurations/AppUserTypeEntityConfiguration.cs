using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetrix.Models;

namespace Monetrix.Configurations
{
    public class AppUserTypeEntityConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasIndex(u => u.NationalId).IsUnique();
            builder.Property(u => u.NationalId).HasMaxLength(14);

            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.PhoneNumber).HasMaxLength(11);

            builder.Property(u => u.Position).HasMaxLength(15);
            builder.Property(u => u.Position).HasConversion<string>();

            builder.Property(u => u.FullName).HasMaxLength(60).UseCollation("Arabic_CI_AS");

            builder.HasMany(u => u.Loans)
                .WithOne(l => l.AppUser)
                .HasForeignKey(l => l.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Transactions)
                .WithOne(t => t.AppUser)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
