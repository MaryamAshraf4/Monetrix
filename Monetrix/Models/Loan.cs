using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class Loan
    {
        public int LoanId { get; set; }

        [Range(0.01, 999999999999999.99, ErrorMessage = "Amount must be a positive number.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Interest rate is required.")]
        [Range(0.01, 100.00, ErrorMessage = "Interest rate must be between 0.01% and 100%.")]
        [Display(Name = "Interest Rate")]
        public decimal InterestRate { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Pending;

        [MaxLength(100, ErrorMessage = "Purpose is too long.")]
        public string Purpose { get; set; } = string.Empty;

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; } = string.Empty;

        [Display(Name = "Loan Officer")]
        public AppUser AppUser { get; set; } = null!;
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public Loan()
        {
            StartDate = DateTime.UtcNow;
            EndDate = DateTime.UtcNow.AddYears(1);
        }
    }
}
