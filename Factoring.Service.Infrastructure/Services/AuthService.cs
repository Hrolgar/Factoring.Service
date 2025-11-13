using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Factoring.Service.Application.Exceptions;
using Factoring.Service.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Factoring.Service.Infrastructure.Services;

public class AuthService(IConfiguration configuration) : IAuthService
{
    // In a real app, this would come from a database.
    private const string ValidUsername = "admin";
    private const string ValidPassword = "password";
    

    public string Authenticate(string username, string password)
    {
        if (username != ValidUsername || password != ValidPassword)
        {
            Console.WriteLine("Authentication failed for user: " + username);

            return "";
        }

        return GenerateJwtToken(username);
    }

    public string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"] ??
                                          throw new NotFoundException(nameof(AuthService), "Jwt Key not found"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, "some_user_id"),
                new Claim(JwtRegisteredClaimNames.Name, "testuser"),
                new Claim("role", "Admin") // Example of a custom role claim
            ]),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}