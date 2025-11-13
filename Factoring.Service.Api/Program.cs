using Factoring.Service.Application.Invoices.Queries;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Interfaces.IExternalServices;
using Factoring.Service.Infrastructure.Data;
using Factoring.Service.Infrastructure.ExternalServices.Rest;
using Factoring.Service.Infrastructure.Mediator;
using Factoring.Service.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Factoring Service API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddFactoringMediator(typeof(Factoring.Service.Application.Common.IMediator).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICreditCheckService, CreditCheckService>();



builder.Services.AddAutoMapper(cfg => { },
    typeof(AutoMapperProfile).Assembly);

var retryPolicy = HttpPolicyExtensions
    .HandleTransientHttpError()
    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));


var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(2); // 2-second timeout


builder.Services.AddHttpClient<IInvoiceRegistrationService, InvoiceRegistrationService>(client =>
    {
        client.BaseAddress = new Uri("https://reqres.in/");
    })
    .AddPolicyHandler(retryPolicy);



builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
    {
        client.BaseAddress = new Uri("https://api.frankfurter.app/");
    })
    .AddPolicyHandler(timeoutPolicy);

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();