using System.Net;
using Microsoft.IdentityModel.Tokens;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Services.RestEaseServices;

namespace Project.ECommerce.API.Users.src.Middlewares;
public class AuthenticationMiddleware(RequestDelegate next, IAuthenticationRestEaseService authenticationRestEase)
{
    private readonly RequestDelegate _next = next;
    private readonly IAuthenticationRestEaseService _authenticationRestEase = authenticationRestEase;

    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;

        var tokenInHeader = request.Headers.Authorization;

        if (tokenInHeader.IsNullOrEmpty())
        {
            throw new ApiException(Constants.UNAUTHORIZED, HttpStatusCode.Unauthorized);
        }

        var tokenIsValid = await _authenticationRestEase.VerifyTokenValidAsync(tokenInHeader);

        if (tokenIsValid.StatusCode == HttpStatusCode.OK)
        {
            await _next(httpContext);
        }
        else
        {
            throw new ApiException(Constants.UNAUTHORIZED, HttpStatusCode.Unauthorized);
        }
    }
}