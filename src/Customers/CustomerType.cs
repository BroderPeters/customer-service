using CodeChallenge.CustomerService.Data;
using HotChocolate.Types;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerType : ObjectType<Customer>
    {
        protected override void Configure(IObjectTypeDescriptor<Customer> descriptor)
        {
            descriptor.Description("A simple customer.");

            descriptor
                .Field(x => x.Id)
                .Type<LongType>()
                .Description("The unique Id of the customer.");

            descriptor
                .Field(x => x.Email)
                .Type<EmailAddressType>()
                .Description("The E-Mail of the customer.");

            descriptor
                .Field(x => x.Name)
                .Type<NonEmptyStringType>()
                .Description("The Name of the customer.");

            descriptor
                .Field(x => x.Code)
                .Type<IntType>()
                .Description("The Code of the customer.");

            descriptor
                .Field(x => x.Status)
                .Type<CustomerStatusType>()
                .Description("The Status of the customer, deterring whether he is Active or Inactive.");

            descriptor
                .Field(x => x.IsBlocked)
                .Type<BooleanType>()
                .Description("Defines whether the customer is blocked.");

            descriptor
                .Field(x => x.CreatedAt)
                .Type<DateTimeType>()
                .Description("The creation date and time of the customer data record in UTC.");
        }
    }
}
