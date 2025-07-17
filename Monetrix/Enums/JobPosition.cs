using System.ComponentModel.DataAnnotations;

namespace Monetrix.Enums
{
    public enum JobPosition
    {
        Admin,

        [Display(Name = "Customer Service")]
        CustomerService,

        [Display(Name = "Bank Teller")]
        Teller,

        [Display(Name = "Accountant")]
        Accountant,

        [Display(Name = "Loan Officer")]
        LoanOfficer,

        [Display(Name = "Auditor")]
        Auditor
    }
}