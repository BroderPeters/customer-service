using System.Collections.Generic;
using CodeChallenge.CustomerService.Common;
using CodeChallenge.CustomerService.Data;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerPayloadBase : Payload
    {
        protected CustomerPayloadBase(Customer customer)
        {
            Customer = customer;
        }

        protected CustomerPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Customer? Customer { get; }
    }
}
