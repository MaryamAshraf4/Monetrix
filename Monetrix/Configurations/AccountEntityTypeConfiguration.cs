using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetrix.Models;

namespace Monetrix.Configurations
{
    public class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasIndex(a => a.AccountNumber).IsUnique();

            builder.Property(a => a.AccountType).HasMaxLength(15);

            builder.Property(a => a.AccountType).HasConversion<string>();

            builder.HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(a => a.TransactionsSent)
                .WithOne(t => t.SenderAccount)
                .HasForeignKey(t => t.SenderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.TransactionsReceived)
                .WithOne(t => t.ReceiverAccount)
                .HasForeignKey(t => t.ReceiverAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
