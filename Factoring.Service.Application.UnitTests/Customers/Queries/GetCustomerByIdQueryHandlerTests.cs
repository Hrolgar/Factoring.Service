using Factoring.Service.Application.Customers.Queries;
using Factoring.Service.Application.Exceptions;
using Factoring.Service.Core.Interfaces;
using Factoring.Service.Core.Models;
using FluentAssertions;
using NSubstitute;

namespace Factoring.Service.Application.UnitTests.Customers.Queries;

public class GetCustomerByIdQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

    [Fact]
    public async Task Handle_ShouldReturnCustomerDto_WhenCustomerExists()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var customer = new Customer { Id = customerId, Name = "Test Customer" };
        _unitOfWork.Customers.GetByIdAsync(customerId).Returns(customer);

        var handler = new GetCustomerByIdQueryHandler(_unitOfWork);
        var query = new GetCustomerById(customerId);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(customerId);
        result.Name.Should().Be("Test Customer");
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        _unitOfWork.Customers.GetByIdAsync(customerId).Returns((Customer)null);

        var handler = new GetCustomerByIdQueryHandler(_unitOfWork);
        var query = new GetCustomerById(customerId);

        // Act
        Func<Task> act = async () => await handler.Handle(query, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}