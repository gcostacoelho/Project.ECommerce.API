using System.Net;
using Project.ECommerce.API.Authentication.src.Facades.Interfaces;
using Project.ECommerce.API.Authentication.src.Models.Utils;
using Project.ECommerce.API.Authentication.src.Repositories.Interfaces;
using Project.ECommerce.API.Authentication.src.Services.Interfaces;

namespace Project.ECommerce.API.Authentication.src.Facades;
public class AuthenticationFacade(ILoginRepository loginRepository, ITokenServices tokenServices) : IAuthenticationFacade
{
    private readonly ILoginRepository _loginRepository = loginRepository;
    private readonly ITokenServices _tokenServices = tokenServices;

    public async Task<ApiResponse<string>> CreateTokenAsync(string email, string password)
    {
        var loginInfos = await _loginRepository.GetLoginInfos(email) ?? throw new ApiException("User not found", HttpStatusCode.BadRequest);

        if (loginInfos.Password != password)
        {
            throw new ApiException("Unauthorized", HttpStatusCode.Unauthorized);
        }

        var token = _tokenServices.CreateTokenAsync(loginInfos.UserId.ToString(), loginInfos.Email);

        return ApiResponse<string>.Success(token);
    }

    public ApiResponse<string> ValidateToken(string token)
    {
        if (token is null)
        {
            throw new ApiException("Unauthorized", HttpStatusCode.Unauthorized);
        }

        var isValid = _tokenServices.ValidateTokenAsync(token);

        if (isValid is null)
        {
            throw new ApiException("Unauthorized", HttpStatusCode.Unauthorized);
        }

        return ApiResponse<string>.Success("Token valid");
    }
}