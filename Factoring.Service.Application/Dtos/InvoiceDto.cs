using Factoring.Service.Core.Enums;

namespace Factoring.Service.Application.Dtos;

public class InvoiceDto
{
    public Guid Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public string?  Status { get; set; }
}