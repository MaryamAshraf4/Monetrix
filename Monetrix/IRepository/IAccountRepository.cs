using Monetrix.Models;

namespace Monetrix.IRepository
{
    public interface IAccountRepository
    {
        Task<Account> GetAccountByIdAsync(int id);
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task CreateAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
    }
}
