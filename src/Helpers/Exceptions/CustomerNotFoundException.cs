using System;

namespace CodeChallenge.CustomerService.Helpers.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message)
        {
        }
    }
}