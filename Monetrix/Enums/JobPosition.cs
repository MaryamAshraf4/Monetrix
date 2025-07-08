using System.ComponentModel.DataAnnotations;

namespace Monetrix.Enums
{
    public enum JobPosition
    {
        [Display(Name = "Bank Teller")]
        Teller,

        [Display(Name = "Branch Manager")]
        Manager,

        [Display(Name = "Accountant")]
        Accountant,

        [Display(Name = "Office Clerk")]
        Clerk,

        [Display(Name = "Loan Officer")]
        LoanOfficer
    }
}