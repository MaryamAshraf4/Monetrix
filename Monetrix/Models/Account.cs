using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public bool IsActive{ get; set; } = true;

        [Range(0.01, 999999999999999.99, ErrorMessage = "Amount must be a positive number.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Account number is required.")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Account number must be between 10 and 20 characters.")]
        public string AccountNumber { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid account type.")]
        public AccountType AccountType { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? CloseDate { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<Transaction> TransactionsSent { get; set; } = new List<Transaction>();

        public ICollection<Transaction> TransactionsReceived { get; set; } = new List<Transaction>();
    }
}
