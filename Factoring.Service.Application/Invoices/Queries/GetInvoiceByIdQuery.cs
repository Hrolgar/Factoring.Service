using AutoMapper;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Application.Common;
using Factoring.Service.Core.Interfaces;

namespace Factoring.Service.Application.Invoices.Queries;

public record GetInvoiceByIdQuery (Guid Id) : IRequest<InvoiceDto>;

public class GetInvoiceByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
{
    public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice =  await unitOfWork.Invoices.GetByIdAsync(request.Id);
        var invoiceDto = mapper.Map<InvoiceDto>(invoice);
        return invoiceDto;
    }
}