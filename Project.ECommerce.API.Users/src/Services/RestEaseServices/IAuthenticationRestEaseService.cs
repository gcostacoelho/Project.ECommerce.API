using RestEase;

namespace Project.ECommerce.API.Users.src.Services.RestEaseServices;
public interface IAuthenticationRestEaseService
{
    [AllowAnyStatusCode]
    [Get("/api/Authentication/validateToken/{token}")]
    Task<HttpResponseMessage> VerifyTokenValidAsync([Path] string token); 
}