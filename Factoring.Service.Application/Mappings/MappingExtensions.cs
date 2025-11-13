using Factoring.Service.Application.Dtos;
using Factoring.Service.Core.Models;

namespace Factoring.Service.Application.Mappings;

public static class MappingExtensions
{
    public static CustomerDto ToDto(this Customer customer)
    {
        return new CustomerDto
        {
            Id = customer.Id,
            Name = customer.Name,
            OrganizationNumber = customer.OrganizationNumber,
            CreditScore = customer.CreditScore
        };
    }
    
    public static IEnumerable<CustomerDto> ToDto(this IEnumerable<Customer> customers) => customers.Select(c => c.ToDto());
    
    public static InvoiceDto ToDto(this Invoice invoice)
    {
        return new InvoiceDto
        {
            Id = invoice.Id,
            CustomerId = invoice.CustomerId,
            Amount = invoice.Amount,
            Currency = invoice.Currency,
            Status = invoice.Status.ToString(),
            IssuedDate = invoice.IssuedDate,
            DueDate = invoice.DueDate,
        };
    }
    
    public static IEnumerable<InvoiceDto> ToDto(this IEnumerable<Invoice> invoices) => invoices.Select(i => i.ToDto());
}