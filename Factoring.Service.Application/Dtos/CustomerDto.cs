namespace Factoring.Service.Application.Dtos;

public record CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string? OrganizationNumber { get; set; }
    public int? CreditScore { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
}