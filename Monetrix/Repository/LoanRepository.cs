using Microsoft.EntityFrameworkCore;
using Monetrix.IRepository;
using Monetrix.Models;
using System;

namespace Monetrix.Repository
{
    public class LoanRepository : ILoanRepository
    {
        private readonly AppDbContext _context;
        public LoanRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddLoanAsync(Loan loan)
        {
            _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
        }

        public async Task CompletedLoanAsync(int id)
        {
           var loan = await GetLoanByIdAsync(id);
            if (loan != null)
            {
                loan.Status = Enums.LoanStatus.Completed;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans.Include(l => l.AppUser).ToListAsync();
        }

        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _context.Loans.Include(l => l.AppUser).Include(l => l.Customer).FirstOrDefaultAsync(l => l.LoanId == id);
        }

        public async Task UpdateLoanAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }
    }
}
