using Factoring.Service.Application.Dtos;
using Factoring.Service.Application.Mappings;
using Factoring.Service.Core.Models;
using FluentAssertions;

namespace Factoring.Service.Application.UnitTests;

public class MappingExtensionsTests
{
    [Fact]
    public void ToDto_ShouldMapCustomerToCustomerDto_WhenCustomerIsValid()
    {
        // Arrange
        var customer = new Customer
        {
            Id = Guid.NewGuid(),
            Name = "Test Corp",
            OrganizationNumber = "1234567890",
            CreditScore = 1000,
        };
        
        // Act
        var customerDto = customer.ToDto();
        
        // Assert
        customerDto.Should().NotBeNull();
        customerDto.Should().BeOfType<CustomerDto>();
        customerDto.Id.Should().Be(customer.Id);
        customerDto.Name.Should().Be(customer.Name);
    }
    
}