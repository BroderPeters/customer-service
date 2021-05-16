using HotChocolate.Types;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerMutationsType : ObjectType<CustomerMutations>
    {
        protected override void Configure(IObjectTypeDescriptor<CustomerMutations> descriptor)
        {
            descriptor
                .Field(x => x.AddCustomer(default!, default!))
                .Description("Add a customer.");

            descriptor
                .Field(x => x.UpdateCustomer(default!, default!))
                .Description("Update a customer.");

            descriptor
                .Field(x => x.DeleteCustomer(default!, default!))
                .Description("Delete a customer.");
        }
    }
}
