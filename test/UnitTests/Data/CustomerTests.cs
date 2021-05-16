using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Helpers.Exceptions;
using Xunit;

namespace CodeChallenge.UnitTests.Data
{
    public class CustomerTests
    {
        [Fact]
        public void SetEmail_MaxStringLengthExceeded_ThrowMaxStringLengthExceededException()
        {
            Assert.Throws<MaxStringLengthExceededException>(
                () => new Customer().SetEmail(
                    "prettylongemailaddressprettylongemailaddressprettylongemailaddressprettylongemailaddressprettylongemailaddress@prettylongemailaddress.com"));
        }

        [Fact]
        public void SetName_MaxStringLengthExceeded_ThrowMaxStringLengthExceededException()
        {
            Assert.Throws<MaxStringLengthExceededException>(
                () => new Customer().SetName(
                    "prettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongname"));
        }
    }
}