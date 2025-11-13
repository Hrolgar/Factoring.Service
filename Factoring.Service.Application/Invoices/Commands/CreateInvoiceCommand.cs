using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;
using MediatR;

namespace Factoring.Service.Application.Invoices.Commands;

public class CreateInvoiceCommand : IRequest<Guid>
{
    public Guid CustomerId { get; set; }
    public string? InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}

public class CreateInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateInvoiceCommand, Guid>
{
    public async Task<Guid> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.CustomerId);
        if (customer == null)
            throw new InvalidOperationException($"Customer {request.CustomerId} does not exist.");
        var invoice = new Invoice
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            InvoiceNumber = request.InvoiceNumber ?? string.Empty,
            Amount = request.Amount,
            DueDate = request.DueDate,
            IssuedDate = DateTime.UtcNow,
            CreatedOn = DateTime.UtcNow,
            ModifiedOn = DateTime.UtcNow
        };

        await unitOfWork.Invoices.AddAsync(invoice);
        await unitOfWork.CompleteAsync();
        
        return invoice.Id;
    }        
}