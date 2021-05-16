using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.CustomerService.Data;

namespace CodeChallenge.CustomerService.Repositories
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetCustomers();
        Task<List<Customer>> GetCustomerByIds(IReadOnlyList<long> keys);
        Task<Customer> GetCustomerById(long id);
        Task AddCustomer(Customer customer);
        void RemoveCustomer(Customer customer);
        Task SaveChanges();
    }
}
