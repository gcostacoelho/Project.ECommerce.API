using Project.ECommerce.API.Authentication.src.Models.Utils;

namespace Project.ECommerce.API.Authentication.src.Facades.Interfaces;
public interface IAuthenticationFacade
{
    Task<ApiResponse<string>> CreateTokenAsync(string email, string password);
}