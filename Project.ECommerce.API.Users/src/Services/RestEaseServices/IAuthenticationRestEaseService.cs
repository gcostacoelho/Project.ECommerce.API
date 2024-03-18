using Project.ECommerce.API.Users.src.Models.RestEaseResponses;
using RestEase;

namespace Project.ECommerce.API.Users.src.Services.RestEaseServices;
public interface IAuthenticationRestEaseService
{
    [AllowAnyStatusCode]
    [Get("/api/Authentication/validateToken/{token}")]
    Task<VerifyTokenValidResponse> VerifyTokenValidAsync([Path] string token);
}