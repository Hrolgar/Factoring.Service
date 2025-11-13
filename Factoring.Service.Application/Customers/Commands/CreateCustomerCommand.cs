using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;
using MediatR;

namespace Factoring.Service.Application.Customers.Commands;

public record CreateCustomerCommand(string Name, string? OrganizationNumber) : IRequest<Guid>;

public class CreateCustomerCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCustomerCommand, Guid>
{
    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
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