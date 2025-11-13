using Factoring.Service.Application.Dtos;
using Factoring.Service.Application.Common;
using Factoring.Service.Application.Exceptions;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;

namespace Factoring.Service.Application.Invoices.Queries;

public record GetInvoiceByIdQuery (Guid Id) : IRequest<InvoiceDto>;

public class GetInvoiceByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInvoiceByIdQuery, InvoiceDto>
{
    public async Task<InvoiceDto> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var invoice =  await unitOfWork.Invoices.GetByIdAsync(request.Id);
        if (invoice == null)
            throw new NotFoundException(nameof(Invoice), request.Id);
        return invoice.ToDto();
    }
}