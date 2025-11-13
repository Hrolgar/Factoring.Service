namespace Factoring.Service.Core.Models;

public class Customer
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? OrganizationNumber { get; set; }
    public int? CreditScore { get; set; }
    public List<Invoice> Invoices { get; set; } = [];

    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
    

}