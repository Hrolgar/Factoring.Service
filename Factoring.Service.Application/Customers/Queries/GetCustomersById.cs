using Factoring.Service.Application.Common;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Interfaces;

namespace Factoring.Service.Application.Customers.Queries;

public record GetCustomerById (Guid Id) : IRequest<CustomerDto>;


public class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCustomerById, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.Id);
        if (customer == null) throw new InvalidOperationException($"Customer {request.Id} does not exist.");
        return customer.ToDto();
    }
}
