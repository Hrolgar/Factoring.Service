namespace Factoring.Service.Core.Interfaces.IExternalServices;

public interface IExchangeRateService
{
    Task<decimal?> GetExchangeRateAsync(string fromCurrency, string toCurrency = "USD");
}