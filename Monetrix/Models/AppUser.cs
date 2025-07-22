using Microsoft.AspNetCore.Identity;
using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.Models
{
    public class AppUser : IdentityUser
    {
        public string NationalId { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a position.")]
        public JobPosition Position { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }
        public bool IsFirstLogin { get; set; } = true;
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
