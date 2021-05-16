using System;

namespace CodeChallenge.CustomerService.Helpers.Exceptions
{
    [Serializable]
    public class MaxStringLengthExceededException : Exception
    {
        public MaxStringLengthExceededException(string message) : base(message) { }
    }
}