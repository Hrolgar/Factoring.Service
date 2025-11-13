using Factoring.Service.Application.Common;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Application.Exceptions;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;

namespace Factoring.Service.Application.Customers.Queries;

public record GetCustomerById (Guid Id) : IRequest<CustomerDto>;


public class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCustomerById, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.Id);
        if (customer == null) throw new NotFoundException(nameof(Customer), request.Id);
        return customer.ToDto();
    }
}
