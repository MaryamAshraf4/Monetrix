using Monetrix.Models;
using Monetrix.ViewModels;

namespace Monetrix.IRepository
{
    public interface ITransactionRepository
    {
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<bool> CreateTransactionAsync(TransactionViewModel transactionVm, string userId);
        Task<bool> ReverseTransactionAsync(int transactionId, string userId);
    }
}
