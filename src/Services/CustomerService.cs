using System.Threading.Tasks;
using CodeChallenge.CustomerService.Customers;
using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Helpers.Exceptions;
using CodeChallenge.CustomerService.Repositories;

namespace CodeChallenge.CustomerService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> AddCustomer(AddCustomerInput addCustomerInput)
        {
            var customer = new Customer
            {
                Code = addCustomerInput.Code,
                Status = addCustomerInput.Status,
                IsBlocked = addCustomerInput.IsBlocked
            };

            customer.SetEmail(addCustomerInput.Email);
            customer.SetName(addCustomerInput.Name);

            await _repository.AddCustomer(customer);
            await _repository.SaveChanges();

            return customer;
        }

        public async Task<Customer> UpdateCustomer(UpdateCustomerInput updateCustomerInput)
        {
            var customer = await _repository.GetCustomerById(updateCustomerInput.id);

            if (customer is null)
            {
                throw new CustomerNotFoundException($"Customer with id {updateCustomerInput.id} not found.");
            }

            customer.Code = updateCustomerInput.Code;
            customer.Status = updateCustomerInput.Status;
            customer.IsBlocked = updateCustomerInput.IsBlocked;

            customer.SetEmail(updateCustomerInput.Email);
            customer.SetName(updateCustomerInput.Name);

            await _repository.SaveChanges();

            return customer;
        }

        public async Task<Customer> DeleteCustomer(DeleteCustomerInput deleteCustomerInput)
        {
            var customer = await _repository.GetCustomerById(deleteCustomerInput.id);

            if (customer is null)
            {
                throw new CustomerNotFoundException($"Customer with id {deleteCustomerInput.id} not found.");
            }

            _repository.RemoveCustomer(customer);
            await _repository.SaveChanges();

            return customer;
        }
    }
}
