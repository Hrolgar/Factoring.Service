using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Interfaces.IExternalServices;
using Factoring.Service.Application.Common;
using Factoring.Service.Application.Exceptions;

namespace Factoring.Service.Application.Invoices.Commands;

public record FinanceInvoiceCommand (Guid Id) : IRequest;

public class FinanceInvoiceCommandHandler(IUnitOfWork unitOfWork, ICreditCheckService creditCheckService, IExchangeRateService exchangeRateService, IInvoiceRegistrationService invoiceRegistrationService) 
    : IRequestHandler<FinanceInvoiceCommand>
{
    public async Task Handle(FinanceInvoiceCommand request, CancellationToken cancellationToken)
    {
        var invoice = await unitOfWork.Invoices.GetByIdAsync(request.Id);
        if (invoice == null)
            throw new NotFoundException(nameof(Core.Models.Invoice), request.Id);
        
        var isCreditWorthy = await creditCheckService.IsCustomerCreditWorthyAsync(invoice.CustomerId);
        if (!isCreditWorthy) 
            throw new ValidationException($"Customer ({invoice.CustomerId}) credit score is too low.");

        if (invoice.Currency is not null && invoice.Currency != "USD")
        {
            var rate = await exchangeRateService.GetExchangeRateAsync(invoice.Currency, "USD");  
            Console.WriteLine($"Financed invoice {invoice.Id}. Exchange rate from {invoice.Currency} to USD is {rate}.");
            invoice.Currency = "USD";
        } 

        invoice.MarkAsFinanced();
        var registrationId = await invoiceRegistrationService.RegisterFinancedInvoiceAsync(invoice);
        Console.WriteLine($"Invoice {invoice.Id} financed. Registration ID: {registrationId}");
        await unitOfWork.CompleteAsync();
    }


}