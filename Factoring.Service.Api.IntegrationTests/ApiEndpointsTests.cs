using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.TestHost;

namespace Factoring.Service.Api.IntegrationTests;

public class ApiEndpointsTests(CustomWebAppFactory factory) : IClassFixture<CustomWebAppFactory>
{
    private readonly CustomWebAppFactory _factory = factory;

    private HttpClient CreateAuthenticatedClient()
    {
        return _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication(TestAuthHandler.TestSchemeName)
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.TestSchemeName, _ => { });
            });
        }).CreateClient();
    }

    [Fact]
    public async Task GetCustomers_WithoutToken_ShouldReturnUnauthorized()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/api/customers");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetCustomerById_WithAuthenticatedUser_ShouldReturnNotFoundForInvalidId()
    {
        // Arrange
        var client = CreateAuthenticatedClient();

        // Act
        var response = await client.GetAsync($"/api/customers/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}