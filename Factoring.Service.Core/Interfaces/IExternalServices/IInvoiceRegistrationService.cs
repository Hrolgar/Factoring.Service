using Factoring.Service.Core.Models;

namespace Factoring.Service.Core.Interfaces.IExternalServices;

public interface IInvoiceRegistrationService
{
    Task<string?> RegisterFinancedInvoiceAsync(Invoice invoice);
}