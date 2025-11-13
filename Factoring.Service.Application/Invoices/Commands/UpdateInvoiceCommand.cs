using System.Text.Json.Serialization;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Application.Common;

namespace Factoring.Service.Application.Invoices.Commands;

public class UpdateInvoiceCommand : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string? InvoiceNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
}

public class UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateInvoiceCommand, bool>
{
    public async Task<bool> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await unitOfWork.Invoices.GetByIdAsync(request.Id);
        if (invoice == null)
            return false;

        invoice.InvoiceNumber = request.InvoiceNumber ?? string.Empty;
        invoice.Amount = request.Amount;
        invoice.DueDate = request.DueDate;
        invoice.ModifiedOn = DateTime.UtcNow;

        await unitOfWork.CompleteAsync();
        return true;
    }
}