using Monetrix.Models;

namespace Monetrix.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer?> GetCustomerByIdWithAccountsAndLoansAsync(int id);
        Task<IEnumerable<Customer>> GetAllCustomersAsync(string? searchString);
        Task<IEnumerable<Customer>> GetAllArchivedCustomersAsync(string? searchString);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
