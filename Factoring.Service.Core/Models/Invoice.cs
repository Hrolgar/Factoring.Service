using Factoring.Service.Core.Enums;

namespace Factoring.Service.Core.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    public InvoiceStatus  Status { get; private set; }
    
    // Navigation properties
    // public Customer? Customer { get; set; } = null;
    
    public void MarkAsPaid()
    {
        Status = Status is InvoiceStatus.Issued or InvoiceStatus.Financed 
            ? InvoiceStatus.Paid
            : throw new InvalidOperationException("Cannot mark invoice as paid when it is not in Issued or Financed status.");
        
    }
    
    public void MarkAsFinanced()
    {
        Status = Status is InvoiceStatus.Issued
            ? InvoiceStatus.Financed
            : throw new InvalidOperationException("Cannot mark invoice as financed when it is not Issued");
    }
}