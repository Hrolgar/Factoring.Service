using Factoring.Service.Core.Interfaces;
using Factoring.Service.Application.Common;

namespace Factoring.Service.Application.Invoices.Commands;

public class DeleteInvoiceCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteInvoiceCommand, bool>

{
    public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await unitOfWork.Invoices.GetByIdAsync(request.Id);
        if (invoice == null) return false;

        unitOfWork.Invoices.Remove(invoice);
        await unitOfWork.CompleteAsync();
        return true;
    }
}