using Factoring.Service.Application.Common;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;

namespace Factoring.Service.Application.Customers.Commands;

// public record CreateCustomerCommand(string Name, string? OrganizationNumber) : IRequest<Guid>;
public record CreateCustomerRequest(string Name, string? OrganizationNumber) : IRequest<Guid>;

public class CreateCustomerRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerRequest, Guid>
{
    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            OrganizationNumber = request.OrganizationNumber,
            CreditScore = 600 // Default credit score
        };
        
        await unitOfWork.Customers.AddAsync(customer);
        
        await unitOfWork.CompleteAsync();
        return customer.Id;
    }
}