using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Monetrix.Models;

namespace Monetrix.Configurations
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {  
            builder.Property(t => t.TransactionType).HasMaxLength(12).HasConversion<string>();

            builder.Property(t => t.Description).HasMaxLength(200);

            builder.HasOne(t => t.SenderAccount)
                .WithMany(a => a.TransactionsSent)
                .HasForeignKey(t => t.SenderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.ReceiverAccount)
                .WithMany(a => a.TransactionsReceived)
                .HasForeignKey(t => t.ReceiverAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.AppUser)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
