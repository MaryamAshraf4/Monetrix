using Monetrix.Models;

namespace Monetrix.IRepository
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountByIdAsync(int id);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account?> GetAccountByNumberAsync(string accountNumber);
        Task<Account?> GetAccouuntByIdWithTransactionAsync(int id);
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task CloseAccountAsync(int id);
    }
}
