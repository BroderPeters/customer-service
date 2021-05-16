using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeChallenge.CustomerService.Data;
using CodeChallenge.CustomerService.Repositories;
using HotChocolate;
using HotChocolate.Resolvers;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerQueries
    {
        public IQueryable<Customer> GetCustomers([Service] ICustomerRepository repository)
            => repository.GetCustomers();

        public Task<Customer> GetCustomer(
            long id,
            IResolverContext context,
            CancellationToken cancellationToken,
            [Service] ICustomerRepository repository)
        {
            return context.BatchDataLoader<long, Customer>(
                async (keys, ct) =>
                {
                    var result = await repository.GetCustomerByIds(keys);
                    return result.ToDictionary(x => x.Id);
                })
                .LoadAsync(id, cancellationToken);
        }

    }
}
