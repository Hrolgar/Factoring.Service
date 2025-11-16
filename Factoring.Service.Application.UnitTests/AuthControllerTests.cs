using Factoring.Service.Api.Controllers;
using Factoring.Service.Core.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Factoring.Service.Application.UnitTests;

public class AuthControllerTests
{
    [Fact]
    public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
    {
        // Arrange
        var mockAuthService = Substitute.For<IAuthService>();
        var loginRequest = new AuthController.LoginRequest("test", "test");
        
        mockAuthService
            .Authenticate(loginRequest.Username, loginRequest.Password)
            .Returns((string?)null);
        
        var controller = new AuthController(mockAuthService);
        
        // Act
        var result = controller.Login(loginRequest);
        
        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }
    
}