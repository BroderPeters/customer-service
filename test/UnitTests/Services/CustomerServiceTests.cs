using System.Threading.Tasks;
using CodeChallenge.CustomerService.Customers;
using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Helpers.Exceptions;
using CodeChallenge.CustomerService.Repositories;
using CodeChallenge.UnitTests.Helpers;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CodeChallenge.UnitTests.Services
{
    public class CustomerServiceTests
    {
        private readonly ICustomerRepository _repository;

        public CustomerServiceTests()
        {
            _repository = A.Fake<ICustomerRepository>();
        }

        [Fact]
        public async Task AddCustomer()
        {
            Customer expectedResult = new CustomerBuilder().JohnDoe();
            AddCustomerInput addCustomerInput = new AddCustomerInputBuilder().JohnDoe();

            var result = await new CustomerService.Services.CustomerService(_repository).AddCustomer(addCustomerInput);

            A.CallTo(() => _repository.AddCustomer(A<Customer>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();
            result.Should().BeEquivalentTo(expectedResult, options => options.Excluding(x => x.Id).Excluding(x => x.CreatedAt));
        }

        [Fact]
        public async Task UpdateCustomer()
        {
            Customer johnDoe = new CustomerBuilder().JohnDoe();
            UpdateCustomerInput updateCustomerInput = new UpdateCustomerInputBuilder().DoeJohn();
            Customer expectedResult = new CustomerBuilder().DoeJohn();

            A.CallTo(() => _repository.GetCustomerById(1)).Returns(johnDoe);

            var result = await new CustomerService.Services.CustomerService(_repository).UpdateCustomer(updateCustomerInput);

            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateCustomer_CustomerDoesntExist_ThrowsCustomerNotFoundException()
        {
            UpdateCustomerInput updateCustomerInput = new UpdateCustomerInputBuilder().DoeJohn();

            A.CallTo(() => _repository.GetCustomerById(1)).Returns(null as Customer);

            await Assert.ThrowsAsync<CustomerNotFoundException>(
                async () => await new CustomerService.Services.CustomerService(_repository)
                    .UpdateCustomer(updateCustomerInput));
        }

        [Fact]
        public async Task DeleteCustomer()
        {
            Customer johnDoe = new CustomerBuilder().JohnDoe();
            DeleteCustomerInput deleteCustomerInput = new DeleteCustomerInputBuilder().WithId(1);

            A.CallTo(() => _repository.GetCustomerById(1)).Returns(johnDoe);

            var result = await new CustomerService.Services.CustomerService(_repository).DeleteCustomer(deleteCustomerInput);

            A.CallTo(() => _repository.RemoveCustomer(A<Customer>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _repository.SaveChanges()).MustHaveHappenedOnceExactly();
            result.Should().BeEquivalentTo(johnDoe);
        }

        [Fact]
        public async Task DeleteCustomer_CustomerDoesntExist_ThrowsCustomerNotFoundException()
        {
            DeleteCustomerInput deleteCustomerInput = new DeleteCustomerInputBuilder().WithId(1);

            A.CallTo(() => _repository.GetCustomerById(1)).Returns(null as Customer);

            await Assert.ThrowsAsync<CustomerNotFoundException>(
                async () => await new CustomerService.Services.CustomerService(_repository)
                    .DeleteCustomer(deleteCustomerInput));
        }
    }
}
