using Microsoft.AspNetCore.Identity;

namespace Monetrix.Models
{
    public class AppUser : IdentityUser
    {
        public string NationalId { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; } = DateTime.UtcNow;
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
