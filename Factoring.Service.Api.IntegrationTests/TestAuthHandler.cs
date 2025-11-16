using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Factoring.Service.Api.IntegrationTests;


public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string TestSchemeName = "TestScheme";

    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, "TestUser"),
            // new Claim(ClaimTypes.Role, "Admin"), // Assume Admin role by default
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestSchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}