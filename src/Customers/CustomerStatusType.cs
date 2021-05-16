using HotChocolate.Types;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.CustomerService.Customers
{
    public class CustomerStatusType : EnumType<CustomerStatus>
    {
        protected override void Configure(IEnumTypeDescriptor<CustomerStatus> descriptor)
        {
            descriptor.Description("The Status of the customer, deterring whether he is active or inactive.");

            descriptor
                .Value(CustomerStatus.Active)
                .Description("The customer is active.");

            descriptor
                .Value(CustomerStatus.Inactive)
                .Description("The customer is inactive.");
        }
    }
}