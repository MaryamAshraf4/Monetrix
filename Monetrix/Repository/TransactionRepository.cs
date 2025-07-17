using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Monetrix.Enums;
using Monetrix.IRepository;
using Monetrix.Models;
using Monetrix.ViewModels;

namespace Monetrix.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly IAccountRepository _accountRepository;

        public TransactionRepository(AppDbContext context, IAccountRepository accountRepository)
        {
            _context = context;
            _accountRepository = accountRepository;
        }
        public async Task<bool> CreateTransactionAsync(TransactionViewModel transactionVm, string userId)
        {
            var senderAccount = await _accountRepository.GetAccountByIdAsync(transactionVm.SenderAccountId); 

            if (senderAccount == null)
                return false;

            Account? receiverAccount = null;
            int? receiverAccountId = null;

            if (transactionVm.TransactionType != TransactionType.Withdrawal)
            {
                receiverAccount = await _accountRepository.GetAccountByNumberAsync(transactionVm.ReceiverAccountNumber);

                if (receiverAccount == null)
                    return false;

                receiverAccountId = receiverAccount.AccountId;
            }

            if ((transactionVm.TransactionType == TransactionType.Transfer || transactionVm.TransactionType == TransactionType.Withdrawal)
                && senderAccount.Balance < transactionVm.Amount)
                return false;

            var transaction = new Transaction
            {
                SenderAccountId = transactionVm.SenderAccountId,
                ReceiverAccountId = receiverAccountId,
                Amount = transactionVm.Amount,
                Description = transactionVm.Description,
                TransactionDate = DateTime.UtcNow,
                TransactionType = transactionVm.TransactionType,
                AppUserId = userId
            };

            _context.Transactions.Add(transaction);

            switch (transactionVm.TransactionType)
            {
                case TransactionType.Deposit:
                    receiverAccount!.Balance += transactionVm.Amount;
                    break;
                case TransactionType.Withdrawal:
                    senderAccount.Balance -= transactionVm.Amount;
                    break;
                case TransactionType.Transfer:
                    senderAccount.Balance -= transactionVm.Amount;
                    receiverAccount!.Balance += transactionVm.Amount;
                    break;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.Transactions.Include(t => t.AppUser).Include(t => t.SenderAccount).Include(t => t.ReceiverAccount).ToListAsync();
        }
        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            return await _context.Transactions.Include(t => t.AppUser).Include(t => t.SenderAccount).Include(t => t.ReceiverAccount)
                .ThenInclude(a => a.Customer).FirstOrDefaultAsync(t => t.TransactionId == id);
        }
        public async Task<bool> ReverseTransactionAsync(int transactionId, string userId)
        {
            var originalTransaction = await GetTransactionByIdAsync(transactionId);

            if (originalTransaction == null)
                return false;

            if (originalTransaction.TransactionType != TransactionType.Transfer || originalTransaction.ReceiverAccountId == null)
                return false;

            var reverseTransaction = new Transaction
            {
                Amount = originalTransaction.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = TransactionType.Transfer,
                Description = $"Reverse of transaction #{originalTransaction.TransactionId}",
                SenderAccountId = originalTransaction.ReceiverAccountId.Value,
                ReceiverAccountId = originalTransaction.SenderAccountId,
                AppUserId = userId
            };

            var sender = await _accountRepository.GetAccountByIdAsync(reverseTransaction.SenderAccountId);
            var receiver = await _accountRepository.GetAccountByIdAsync(reverseTransaction.ReceiverAccountId!.Value);

            if (sender == null || receiver == null || sender.Balance < reverseTransaction.Amount)
                return false;

            sender.Balance -= reverseTransaction.Amount;
            receiver.Balance += reverseTransaction.Amount;

            _context.Transactions.Add(reverseTransaction);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
