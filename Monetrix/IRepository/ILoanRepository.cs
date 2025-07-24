using Monetrix.Models;

namespace Monetrix.IRepository
{
    public interface ILoanRepository
    {
        Task<Loan?> GetLoanByIdAsync(int id);
        Task<IEnumerable<Loan>> GetAllLoansAsync(); 
        Task AddLoanAsync(Loan loan);
        Task UpdateLoanAsync(Loan loan);
        Task CompletedLoanAsync(int id);
    }
}
