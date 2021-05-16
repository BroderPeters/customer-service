using System.Diagnostics.CodeAnalysis;

namespace CodeChallenge.CustomerService.Customers
{
    [SuppressMessage(
        "Naming Rules",
        "IDE1006: Naming rule violation",
        Justification = "Id is a parameter.")]
    [SuppressMessage(
        "Naming Rules",
        "SA1300: Element Must Begin With Uppercase Letter",
        Justification = "Id is a parameter.")]
    public record DeleteCustomerInput(long id);
}
