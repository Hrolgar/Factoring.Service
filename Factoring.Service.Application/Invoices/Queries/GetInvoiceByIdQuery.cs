using Factoring.Service.Application.Dtos;
using MediatR;

namespace Factoring.Service.Application.Invoices.Queries;

public class GetInvoiceByIdQuery : IRequest<InvoiceDto>
{
    public Guid Id { get; set; }
}