using Microsoft.EntityFrameworkCore;
using Monetrix.IRepository;
using Monetrix.Models;

namespace Monetrix.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Account?> GetAccountByNumberAsync(string accountNumber)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
        }
        public async Task<Account?> GetAccouuntByIdWithTransactionAsync(int id)
        {
            return await _context.Accounts.Include(a => a.Customer).Include(a => a.TransactionsSent).ThenInclude(t => t.ReceiverAccount)
               .Include(a => a.TransactionsReceived).ThenInclude(t => t.SenderAccount).FirstOrDefaultAsync(a => a.AccountId == id);
        }
        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.Include(a => a.Customer).FirstOrDefaultAsync(a => a.AccountId == id);
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await GetAccountByIdAsync(id);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAccountAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }
    }
}
