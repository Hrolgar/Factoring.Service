using AutoMapper;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Core.Interfaces;
using MediatR;

namespace Factoring.Service.Application.Invoices.Queries;

public class GetAllInvoicesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
{
    public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await unitOfWork.Invoices.GetAllAsync();
        return mapper.Map<IEnumerable<InvoiceDto>>(invoices);
    }
}