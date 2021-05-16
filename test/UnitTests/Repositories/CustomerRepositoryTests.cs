using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Infrastructure.Contexts;
using CodeChallenge.CustomerService.Repositories;
using CodeChallenge.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;
using Xunit;

namespace CodeChallenge.UnitTests.Repositories
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task GetCustomers_CustomersExist_ReturnsCustomersOrderedById()
        {
            var customers = new List<Customer>
            {
                new CustomerBuilder().JohnDoe().WithId(2),
                new CustomerBuilder().JohnDoe(),
                new CustomerBuilder().JohnDoe().WithId(3)
            };

            using var options = SqliteInMemory.CreateOptions<CustomerDbContext>();
            using var context = new CustomerDbContext(options);
            context.Database.EnsureCreated();

            context.AddRange(customers);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var result = new CustomerRepository(context).GetCustomers().ToList();

            result.Should().BeEquivalentTo(customers.OrderBy(x => x.Id), options => options.WithStrictOrdering());
        }

        [Fact]
        public async Task GetCustomerByIds_CustomersExist_ReturnsCustomers()
        {
            var customers = new List<Customer>
            {
                new CustomerBuilder().JohnDoe(),
                new CustomerBuilder().JohnDoe().WithId(2),
                new CustomerBuilder().JohnDoe().WithId(3)
            };

            var expectedResult = new List<Customer>
            {
                new CustomerBuilder().JohnDoe(),
                new CustomerBuilder().JohnDoe().WithId(2),
            };

            using var options = SqliteInMemory.CreateOptions<CustomerDbContext>();
            using var context = new CustomerDbContext(options);
            context.Database.EnsureCreated();

            context.AddRange(customers);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var result = await new CustomerRepository(context).GetCustomerByIds(new List<long> { 1, 2 });

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetCustomerById_CustomerExist_ReturnsCustomer()
        {
            Customer johnDoe = new CustomerBuilder().JohnDoe();

            using var options = SqliteInMemory.CreateOptions<CustomerDbContext>();
            using var context = new CustomerDbContext(options);
            context.Database.EnsureCreated();

            context.Add(johnDoe);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var result = await new CustomerRepository(context).GetCustomerById(1);

            result.Should().BeEquivalentTo(johnDoe);
        }

        [Fact]
        public async Task AddCustomer_CustomerAdded()
        {
            Customer johnDoe = new CustomerBuilder().JohnDoe();

            using var options = SqliteInMemory.CreateOptions<CustomerDbContext>();
            using var context = new CustomerDbContext(options);
            context.Database.EnsureCreated();

            await new CustomerRepository(context).AddCustomer(johnDoe);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            var result = await context.Customers.SingleOrDefaultAsync(x => x.Id == johnDoe.Id);

            result.Should().BeEquivalentTo(johnDoe);
        }

        [Fact]
        public async Task RemoveCustomer_CustomerRemoved()
        {
            Customer johnDoe = new CustomerBuilder().JohnDoe();

            using var options = SqliteInMemory.CreateOptions<CustomerDbContext>();
            using var context = new CustomerDbContext(options);
            context.Database.EnsureCreated();

            context.Add(johnDoe);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            new CustomerRepository(context).RemoveCustomer(johnDoe);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();

            context.Customers.Any().Should().BeFalse();
        }

        [Fact]
        public async Task SaveChanges_SavesChanges()
        {
            Customer johnDoe = new CustomerBuilder().JohnDoe();

            using var options = SqliteInMemory.CreateOptions<CustomerDbContext>();
            using var context = new CustomerDbContext(options);
            context.Database.EnsureCreated();

            context.Add(johnDoe);
            await new CustomerRepository(context).SaveChanges();
            context.ChangeTracker.Clear();

            context.Customers.Any().Should().BeTrue();
        }
    }
}
