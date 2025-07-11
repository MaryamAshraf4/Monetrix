using System.ComponentModel.DataAnnotations;

namespace Monetrix.Enums
{
    public enum JobPosition
    {
        [Display(Name = "Branch Manager")]
        Manager,

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