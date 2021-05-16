using System.Collections.Generic;
using CodeChallenge.CustomerService.Common;
using CodeChallenge.CustomerService.Data;

namespace CodeChallenge.CustomerService.Customers
{
    public class AddCustomerPayload : CustomerPayloadBase
    {
        public AddCustomerPayload(Customer customer)
            : base(customer)
        {
        }

        public AddCustomerPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}
