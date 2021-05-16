using System.Collections.Generic;
using CodeChallenge.CustomerService.Common;
using CodeChallenge.CustomerService.Data;

namespace CodeChallenge.CustomerService.Customers
{
    public class UpdateCustomerPayload : CustomerPayloadBase
    {
        public UpdateCustomerPayload(Customer customer)
            : base(customer)
        {
        }

        public UpdateCustomerPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
