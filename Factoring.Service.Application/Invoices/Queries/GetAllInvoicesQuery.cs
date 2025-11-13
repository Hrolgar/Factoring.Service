using Factoring.Service.Application.Common;
using Factoring.Service.Application.Dtos;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Interfaces;

namespace Factoring.Service.Application.Invoices.Queries;

public record GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>;

public class GetAllInvoicesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllInvoicesQuery, IEnumerable<InvoiceDto>>
{
    public async Task<IEnumerable<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var invoices = await unitOfWork.Invoices.GetAllAsync();
        return invoices.ToDto();
    }
}