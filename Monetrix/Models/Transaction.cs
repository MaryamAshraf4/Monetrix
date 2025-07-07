using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string TransactionType { get; set; } = string.Empty; // e.g., Deposit, Withdrawal, Transfer
        public string Description { get; set; } = string.Empty;

        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; } = null!;

        [ForeignKey("ReceiverAccount")]
        public int ReceiverAccountId { get; set; }
        public Account ReceiverAccount { get; set; } = null!;

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
    }
}
