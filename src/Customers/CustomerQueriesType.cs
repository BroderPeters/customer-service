using HotChocolate.Types;
using HotChocolate.Types.Pagination;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerQueriesType : ObjectType<CustomerQueries>
    {
        protected override void Configure(IObjectTypeDescriptor<CustomerQueries> descriptor)
        {
            descriptor
                .Field(x => x.GetCustomers(default!))
                .Type<CustomerType>()
                .UsePaging(options: new PagingOptions
                {
                    MaxPageSize = 50
                })
                .UseProjection()
                .Description("Get customers paginated.");

            descriptor
                .Field(x => x.GetCustomer(default, default!, default, default!))
                .Type<CustomerType>()
                .Description("Get a specific customer by id.")
                .Argument(
                    "id",
                    argumentDescriptor => argumentDescriptor.Description("The id to search the customer on."));
        }
    }
}
