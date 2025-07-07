using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class Loan
    {
        public int LoanId { get; set; }
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow.AddYears(1);
        public string Status { get; set; } = "Pending"; // e.g., Pending, Approved, Rejected
        public string Purpose { get; set; } = string.Empty; // e.g., Personal, Business, Education

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
