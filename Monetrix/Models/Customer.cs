using System.ComponentModel.DataAnnotations;

namespace Monetrix.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter your 14-digit National ID.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits.")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required.")]
        [MaxLength(60)]
        [RegularExpression(@"^([a-zA-Z\u0600-\u06FF]+\s){2,}[a-zA-Z\u0600-\u06FF]+$", ErrorMessage = "Full Name must be at least three words and contain only letters.")]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(200, ErrorMessage = "Image path too long.")]
        public string Image { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Enter an 11-digit phone number.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits.")]
        public string Phone { get; set; } = string.Empty;

        [RegularExpression(@"^(Male|Female)$", ErrorMessage = "Must be either 'Male' or 'Female'.")]
        public string Gender { get; set; } = string.Empty;
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
