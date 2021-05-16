using CodeChallenge.CustomerService.Customers;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.UnitTests.Helpers
{
    public class AddCustomerInputBuilder
    {
        private string _email;
        private string _name;
        private int? _code;
        private CustomerStatus _status;
        private bool _isBlocked;

        public static implicit operator AddCustomerInput(AddCustomerInputBuilder builder)
        {
            return builder.Build();
        }

        public AddCustomerInputBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public AddCustomerInputBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public AddCustomerInputBuilder WithCode(int? code)
        {
            _code = code;
            return this;
        }

        public AddCustomerInputBuilder WithStatus(CustomerStatus status)
        {
            _status = status;
            return this;
        }

        public AddCustomerInputBuilder WithIsBlocked(bool isBlocked)
        {
            _isBlocked = isBlocked;
            return this;
        }

        public AddCustomerInput Build() => new(_email, _name, _code, _status, _isBlocked);

        public AddCustomerInputBuilder JohnDoe()
        {
            _email = "john.doe@mail.com";
            _name = "John Doe";
            _code = 1;
            _status = CustomerStatus.Active;
            _isBlocked = true;

            return this;
        }
    }
}