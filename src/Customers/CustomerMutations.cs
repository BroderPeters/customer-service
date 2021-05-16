using System.Collections.Generic;
using System.Threading.Tasks;
using CodeChallenge.CustomerService.Common;
using CodeChallenge.CustomerService.Helpers;
using CodeChallenge.CustomerService.Helpers.Exceptions;
using CodeChallenge.CustomerService.Services;
using HotChocolate;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerMutations
    {
        public async Task<AddCustomerPayload> AddCustomer(
            AddCustomerInput input,
            [Service] ICustomerService customerService)
        {
            try
            {
                var customer = await customerService.AddCustomer(input);

                return new AddCustomerPayload(customer);
            }
            catch (MaxStringLengthExceededException ex)
            {
                return new AddCustomerPayload(new List<UserError> { new UserError(ex.Message, Constants.MaxStringLengthExceeded) });
            }
        }

        public async Task<UpdateCustomerPayload> UpdateCustomer(
            UpdateCustomerInput input,
            [Service] ICustomerService customerService)
        {
            try
            {
                var customer = await customerService.UpdateCustomer(input);

                return new UpdateCustomerPayload(customer);
            }
            catch (CustomerNotFoundException ex)
            {
                return new UpdateCustomerPayload(
                    new List<UserError>
                    {
                        new UserError(ex.Message, Constants.CustomerNotFound)
                    });
            }
            catch (MaxStringLengthExceededException ex)
            {
                return new UpdateCustomerPayload(
                    new List<UserError>
                    {
                        new UserError(ex.Message, Constants.MaxStringLengthExceeded)
                    });
            }
        }

        public async Task<DeleteCustomerPayload> DeleteCustomer(
            DeleteCustomerInput input,
            [Service] ICustomerService customerService)
        {
            try
            {
                var customer = await customerService.DeleteCustomer(input);

                return new DeleteCustomerPayload(customer);
            }
            catch (CustomerNotFoundException ex)
            {
                return new DeleteCustomerPayload(
                    new List<UserError>
                    {
                        new UserError(ex.Message, Constants.CustomerNotFound)
                    });
            }
        }
    }
}
