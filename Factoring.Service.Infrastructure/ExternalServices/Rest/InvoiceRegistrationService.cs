using System.Net.Http.Json;
using Factoring.Service.Core.Interfaces.IExternalServices;
using Factoring.Service.Core.Models;

namespace Factoring.Service.Infrastructure.ExternalServices.Rest;

public class InvoiceRegistrationService(HttpClient httpClient) : IInvoiceRegistrationService
{
    public async Task<string?> RegisterFinancedInvoiceAsync(Invoice invoice)
    {
        var payload = new
        {
            InvoiceNumber = invoice.InvoiceNumber,
            Amount = invoice.Amount
        };

        var response = await httpClient.PostAsJsonAsync("api/users", payload);

        if (!response.IsSuccessStatusCode) return null; 
        var created = await response.Content.ReadFromJsonAsync<CreatedUser>();
        return created?.Id;

    }

    public record CreatedUser(string Name, string Job, string Id, DateTime CreatedAt);

}