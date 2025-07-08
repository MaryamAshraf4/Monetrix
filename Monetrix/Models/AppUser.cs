using Microsoft.AspNetCore.Identity;
using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class AppUser : IdentityUser
    {
        [Required(ErrorMessage = "Please enter your 14-digit National ID.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits.")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^([a-zA-Z\u0600-\u06FF]+\s){2,}[a-zA-Z\u0600-\u06FF]+$", ErrorMessage = "Full Name must be at least three words and contain only letters.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a position.")]
        public JobPosition Position { get; set; }

        [Range(1, 1000000, ErrorMessage = "Salary must be greater than 0.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
