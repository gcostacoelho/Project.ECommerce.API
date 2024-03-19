using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Services.RestEaseServices;

namespace Project.ECommerce.API.Users.src.Authentication;
public class CustomAuthenticationHandler(
    IOptionsMonitor<CustomAuthSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    IAuthenticationRestEaseService authenticationRestEase) : AuthenticationHandler<CustomAuthSchemeOptions>(options, logger, encoder)
{
    private readonly IAuthenticationRestEaseService _authRestEase = authenticationRestEase;

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var token = Request.Headers.Authorization;

        if (token.IsNullOrEmpty())
        {
            return AuthenticateResult.Fail(Constants.UNAUTHORIZED);
        }

        var response = await _authRestEase.VerifyTokenValidAsync(token);

        if (response is null)
        {
            return AuthenticateResult.Fail(Constants.UNAUTHORIZED);
        }

        var ci = new ClaimsIdentity(response.Data);
        var principal = new GenericPrincipal(ci, null);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}