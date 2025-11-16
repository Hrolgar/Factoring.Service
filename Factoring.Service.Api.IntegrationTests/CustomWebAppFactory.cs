using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Linq;
using Factoring.Service.Core.Interfaces.IExternalServices;

namespace Factoring.Service.Api.IntegrationTests;


public class CustomWebAppFactory : WebApplicationFactory<Program>
{
    public readonly IExchangeRateService ExchangeRateServiceMock = Substitute.For<IExchangeRateService>();
    public readonly IInvoiceRegistrationService InvoiceRegistrationServiceMock = Substitute.For<IInvoiceRegistrationService>();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove real services
            var exchangeRateDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IExchangeRateService));
            if (exchangeRateDescriptor != null)
                services.Remove(exchangeRateDescriptor);

            var invoiceRegistrationDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IInvoiceRegistrationService));
            if (invoiceRegistrationDescriptor != null)
                services.Remove(invoiceRegistrationDescriptor);

            // Add mocks
            services.AddScoped(_ => ExchangeRateServiceMock);
            services.AddScoped(_ => InvoiceRegistrationServiceMock);
        });
    }
}