using System.Text;
using Factoring.Service.Api.Middleware;
using Factoring.Service.Application.Exceptions;
using Factoring.Service.Application.Invoices.Queries;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Interfaces.IExternalServices;
using Factoring.Service.Infrastructure.Data;
using Factoring.Service.Infrastructure.ExternalServices.Rest;
using Factoring.Service.Infrastructure.Mediator;
using Factoring.Service.Infrastructure.Repositories;
using Factoring.Service.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new NotFoundException(nameof(Program), "Jwt Key not found"));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = configuration.GetValue<bool>("Jwt:RequireHttpsMetadata");
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = configuration["Jwt:Audience"]
        };
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Factoring Service API",
        Version = "v1"
    });
    
    options.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/api/auth/login", UriKind.Relative)
            }
        },
        Description = "OAuth2 Password Grant"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "OAuth2" } },
            new string[] {}
        }
    });
    
    // options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    // {
    //     Scheme = "Bearer",
    //     BearerFormat = "JWT",
    //     In = Microsoft.OpenApi.Models.ParameterLocation.Header,
    //     Name = "Authorization",
    //     Description = "Enter: Bearer {your token}",
    //     Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http
    // });
    //
    //
    // options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    // {
    //     {
    //         new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    //         {
    //             Reference = new Microsoft.OpenApi.Models.OpenApiReference
    //             {
    //                 Id = "Bearer",
    //                 Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
    //             }
    //         },
    //         Array.Empty<string>()
    //     }
    // });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddFactoringMediator(typeof(Factoring.Service.Application.Common.IMediator).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICreditCheckService, CreditCheckService>();


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
app.UseMiddleware<ErrorHandlingMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();