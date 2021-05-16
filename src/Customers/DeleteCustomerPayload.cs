using System.Collections.Generic;
using CodeChallenge.CustomerService.Common;
using CodeChallenge.CustomerService.Data;

namespace CodeChallenge.CustomerService.Customers
{
    public class DeleteCustomerPayload : CustomerPayloadBase
    {
        public DeleteCustomerPayload(Customer customer)
            : base(customer)
        {
        }

        public DeleteCustomerPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
