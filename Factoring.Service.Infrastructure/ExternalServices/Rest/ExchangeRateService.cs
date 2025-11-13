using System.Net.Http.Json;
using Factoring.Service.Core.Interfaces.IExternalServices;

namespace Factoring.Service.Infrastructure.ExternalServices.Rest;

public class ExchangeRateService(HttpClient httpClient) : IExchangeRateService
{
    
    public async Task<decimal?> GetExchangeRateAsync(string fromCurrency, string toCurrency = "USD")
    {
        var result = await httpClient.GetFromJsonAsync<ExchangeResponse>(
            $"latest?to={toCurrency}&from={fromCurrency}"
        );

        if (result == null || !result.Rates.TryGetValue("USD", out var rate))
            return null;

        return rate;
    }

    public record ExchangeResponse(
        decimal Amount,
        string Base,
        string Date,
        Dictionary<string, decimal> Rates
    );

}