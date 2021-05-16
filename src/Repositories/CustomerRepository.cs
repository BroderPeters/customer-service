using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.CustomerService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Customer> GetCustomers()
        {
            return _context.Customers.OrderBy(x => x.Id);
        }

        public async Task<List<Customer>> GetCustomerByIds(IReadOnlyList<long> keys)
        {
            return await _context.Customers.Where(x => keys.Contains(x.Id)).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(long id)
        {
            return await _context.Customers.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddCustomer(Customer customer)
        {
            await _context.AddAsync(customer);
        }

        public void RemoveCustomer(Customer customer)
        {
            _context.Remove(customer);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
