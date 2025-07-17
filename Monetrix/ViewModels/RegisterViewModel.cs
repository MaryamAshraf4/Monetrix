using Monetrix.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monetrix.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your 14-digit National ID.")]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "National ID must be 14 digits.")]
        [Display(Name = "National ID")]
        public string NationalId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^([a-zA-Z\u0600-\u06FF]+\s){2,}[a-zA-Z\u0600-\u06FF]+$", ErrorMessage = "Full Name must be at least three words and contain only letters.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a position.")]
        public JobPosition Position { get; set; }

        [Range(1, 1000000, ErrorMessage = "Salary must be greater than 0.")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Salary { get; set; }

        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        [Display(Name = "User Name")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
