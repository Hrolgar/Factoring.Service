using Factoring.Service.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Factoring.Service.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    public record LoginRequest(string Username, string Password);
    
    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromForm] LoginRequest request)
    {
        var token = authService.Authenticate(request.Username, request.Password);
        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok(new { access_token = token, token_type = "Bearer" });
    }
    
    [HttpPost("login/json")]
    [AllowAnonymous]
    public IActionResult LoginJson([FromBody] LoginRequest request)
    {
        var token = authService.Authenticate(request.Username, request.Password);
        if (string.IsNullOrEmpty(token))
            return Unauthorized();

        return Ok(new { access_token = token, token_type = "Bearer" });
    }

}