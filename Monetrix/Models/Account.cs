using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public bool IsActive{ get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public DateTime CloseDate { get; set; } = DateTime.UtcNow;

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
