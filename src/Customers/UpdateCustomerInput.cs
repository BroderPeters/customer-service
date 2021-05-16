using HotChocolate;
using HotChocolate.Types;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.CustomerService.Customers
{
    public record UpdateCustomerInput(
        long id,
        [GraphQLType(typeof(EmailAddressType))] string Email,
        [GraphQLType(typeof(NonEmptyStringType))] string Name,
        int? Code,
        CustomerStatus Status,
        bool IsBlocked);
}
