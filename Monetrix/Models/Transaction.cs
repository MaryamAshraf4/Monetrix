using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }

        [Range(0.01, 999999999999999.99, ErrorMessage = "Amount must be a positive number.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid transaction type.")]
        [Display(Name = "Type")]
        public TransactionType TransactionType { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [ForeignKey("SenderAccount")]
        public int SenderAccountId { get; set; }

        [Display(Name = "Sender Account")]
        public Account SenderAccount { get; set; } = null!;

        [ForeignKey("ReceiverAccount")]
        public int? ReceiverAccountId { get; set; }

        [Display(Name = "Receiver Account")]
        public Account ReceiverAccount { get; set; } = null!;

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = string.Empty;

        [Display(Name = "Teller")]
        public AppUser AppUser { get; set; } = null!;
    }
}
