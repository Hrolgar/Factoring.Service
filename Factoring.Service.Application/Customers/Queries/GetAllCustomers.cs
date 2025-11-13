using AutoMapper;
using Factoring.Service.Application.Common;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Core.Interfaces;

namespace Factoring.Service.Application.Customers.Queries;


public record GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>;


public class GetAllCustomersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await unitOfWork.Customers.GetAllAsync();
        return mapper.Map<IEnumerable<CustomerDto>>(customers);
    }
}