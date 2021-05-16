using System.Threading.Tasks;
using CodeChallenge.CustomerService.Customers;
using CodeChallenge.CustomerService.Data;

namespace CodeChallenge.CustomerService.Services
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomer(AddCustomerInput addCustomerInput);
        Task<Customer> UpdateCustomer(UpdateCustomerInput updateCustomerInput);
        Task<Customer> DeleteCustomer(DeleteCustomerInput deleteCustomerInput);
    }
}
