using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PaymentHub.Core.Helpers;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace PaymentHub.Core.Auth;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IConfiguration _configuration;

    public BasicAuthenticationHandler(
        IConfiguration configuration,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock
        ) : base(options, logger, encoder, clock)
    {
        _configuration = configuration;
    }

    private const string ApiKey = "Hub@HIX^1UF:BZ?V;JC";

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (authHeader == null
            || !authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"paymenthub\"");
            
            return Task.FromResult(AuthenticateResult.Fail("Cabeçalho de Autorização Inválido"));
        }

        var token = authHeader.Substring("Basic ".Length).Trim();
        var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        var credentials = credentialstring.Split(':');

        if (!IsValidUserAsync(credentials[0], credentials[1]))
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            Response.Headers.Add("WWW-Authenticate", "Basic realm=\"paymenthub\"");

            return Task.FromResult(AuthenticateResult.Fail("Cabeçalho de Autorização Inválido"));
        }


        var claims = new[] { new Claim("name", credentials[0]), new Claim(ClaimTypes.Role, "Admin") };
        var identity = new ClaimsIdentity(claims, "Basic");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
    }

    public bool IsValidUserAsync(string user, string pwd)
    {
        var userAccessKey = CryptoHelper.Decrypt(Environment.GetEnvironmentVariable("PAYMENT_HUB_ACCESS_KEY")
            ?? _configuration["Auth:Accesskey"], 
            ApiKey);
        var pwdSecretKey = CryptoHelper.Decrypt(Environment.GetEnvironmentVariable("PAYMENT_HUB_SECRET_KEY")
            ?? _configuration["Auth:Secretkey"], 
            ApiKey);


        return userAccessKey.Equals(user)
            && pwdSecretKey.Equals(pwd);
    }
}
