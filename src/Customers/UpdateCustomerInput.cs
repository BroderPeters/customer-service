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
    [SuppressMessage(
        "Naming Rules",
        "IDE1006: Naming rule violation",
        Justification = "Id is a parameter.")]
    [SuppressMessage(
        "Naming Rules",
        "SA1300: Element Must Begin With Uppercase Letter",
        Justification = "Id is a parameter.")]
    public record UpdateCustomerInput(
        long id,
        [GraphQLType(typeof(EmailAddressType))] string Email,
        [GraphQLType(typeof(NonEmptyStringType))] string Name,
        int? Code,
        CustomerStatus Status,
        bool IsBlocked);
}
