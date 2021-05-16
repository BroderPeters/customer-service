using System.Diagnostics.CodeAnalysis;
using HotChocolate;
using HotChocolate.Types;
using static CodeChallenge.CustomerService.Data.Enums;

namespace CodeChallenge.CustomerService.Customers
{
    [SuppressMessage(
        "Usage",
        "CA1801: Review unused parameters",
        Justification = "Parameters are not unused. Warning comes from HotChocolate record type")]
    public record AddCustomerInput(
        [GraphQLType(typeof(EmailAddressType))] string Email,
        [GraphQLType(typeof(NonEmptyStringType))] string Name,
        int? Code,
        CustomerStatus Status,
        bool IsBlocked);
}
