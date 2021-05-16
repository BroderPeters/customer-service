using System;
using CodeChallenge.CustomerService.Data;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.UnitTests.Helpers
{
    public class CustomerBuilder
    {
        private long _id;
        private string _email;
        private string _name;
        private int? _code;
        private CustomerStatus _status;
        private bool _isBlocked;
        private DateTime _createdAt;

        public static implicit operator Customer(CustomerBuilder builder)
        {
            return builder.Build();
        }

        public CustomerBuilder WithId(long id)
        {
            _id = id;
            return this;
        }

        public CustomerBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CustomerBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CustomerBuilder WithCode(int? code)
        {
            _code = code;
            return this;
        }

        public CustomerBuilder WithStatus(CustomerStatus status)
        {
            _status = status;
            return this;
        }

        public CustomerBuilder WithIsBlocked(bool isBlocked)
        {
            _isBlocked = isBlocked;
            return this;
        }

        public CustomerBuilder WithCreatedAt(DateTime createdAt)
        {
            _createdAt = createdAt;
            return this;
        }

        public Customer Build()
        {
            var customer = new Customer
            {
                Id = _id,
                Code = _code,
                Status = _status,
                IsBlocked = _isBlocked,
                CreatedAt = _createdAt
            };

            customer.SetName(_name);
            customer.SetEmail(_email);

            return customer;
        }

        public CustomerBuilder JohnDoe()
        {
            _id = 1;
            _email = "john.doe@mail.com";
            _name = "John Doe";
            _code = 1;
            _status = CustomerStatus.Active;
            _isBlocked = true;

            return this;
        }

        public CustomerBuilder DoeJohn()
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