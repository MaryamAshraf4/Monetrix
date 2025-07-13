using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;

namespace Monetrix.ViewModels
{
    public class TransactionViewModel
    {
        public int SenderAccountId { get; set; }

        [Required]
        [Range(0.01, 999999999999999.99, ErrorMessage = "Amount must be a positive number.")]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public TransactionType TransactionType { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? ReceiverAccountNumber { get; set; }
    }
}
