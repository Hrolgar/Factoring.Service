using Factoring.Service.Application.Dtos;
using MediatR;

namespace Factoring.Service.Application.Invoices.Queries;

public class GetAllInvoicesQuery : IRequest<IEnumerable<InvoiceDto>>
{
}