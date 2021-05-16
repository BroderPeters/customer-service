using CodeChallenge.CustomerService.Customers;

namespace CodeChallenge.UnitTests.Helpers
{
    public class DeleteCustomerInputBuilder
    {
        private long _id;

        public DeleteCustomerInputBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public DeleteCustomerInput Build() => new DeleteCustomerInput(_id);

        public static implicit operator DeleteCustomerInput(DeleteCustomerInputBuilder builder)
        {
            return builder.Build();
        }
    }
}