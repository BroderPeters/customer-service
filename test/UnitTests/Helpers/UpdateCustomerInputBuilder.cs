using CodeChallenge.CustomerService.Customers;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.UnitTests.Helpers
{
    public class UpdateCustomerInputBuilder
    {
        private long _id;
        private string _email;
        private string _name;
        private int? _code;
        private CustomerStatus _status;
        private bool _isBlocked;

        public static implicit operator UpdateCustomerInput(UpdateCustomerInputBuilder builder)
        {
            return builder.Build();
        }

        public UpdateCustomerInputBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public UpdateCustomerInputBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UpdateCustomerInputBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UpdateCustomerInputBuilder WithCode(int? code)
        {
            _code = code;
            return this;
        }

        public UpdateCustomerInputBuilder WithStatus(CustomerStatus status)
        {
            _status = status;
            return this;
        }

        public UpdateCustomerInputBuilder WithIsBlocked(bool isBlocked)
        {
            _isBlocked = isBlocked;
            return this;
        }

        public UpdateCustomerInput Build() => new(_id, _email, _name, _code, _status, _isBlocked);

        public UpdateCustomerInputBuilder DoeJohn()
        {
            _id = 1;
            _email = "doe.john@mail.com";
            _name = "Doe John";
            _code = 2;
            _status = CustomerStatus.Inactive;
            _isBlocked = false;

            return this;
        }
    }
}