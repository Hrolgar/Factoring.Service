using AutoMapper;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Core.Interfaces;
using MediatR;

namespace Factoring.Service.Application.Customers.Queries;

public record GetCustomerById (Guid Id) : IRequest<CustomerDto>;


public class GetCustomerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetCustomerById, CustomerDto>
{
    public async Task<CustomerDto> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.Id);
        return mapper.Map<CustomerDto>(customer);
    }
}
