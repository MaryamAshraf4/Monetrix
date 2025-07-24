using Microsoft.EntityFrameworkCore;
using Monetrix.IRepository;
using Monetrix.Models;

namespace Monetrix.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.IgnoreQueryFilters().FirstOrDefaultAsync(c => c.CustomerId == id);
        }
        public async Task<Customer?> GetCustomerByIdWithAccountsAndLoansAsync(int id)
        {
            return await _context.Customers
                .Include(c => c.Accounts)
                .Include(c => c.Loans)
                .FirstOrDefaultAsync(c => c.CustomerId == id);
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync(string? searchString)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c => c.FullName.Contains(searchString));
            }

            return await query.ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetAllArchivedCustomersAsync(string? searchString)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                query = query.Where(c => c.FullName.Contains(searchString));
            }

            return await query.IgnoreQueryFilters().Where(c => c.IsDeleted).ToListAsync();
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCustomerAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await GetCustomerByIdAsync(id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
