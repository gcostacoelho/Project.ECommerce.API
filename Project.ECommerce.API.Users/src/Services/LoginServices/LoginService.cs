using System.Net;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Services.LoginServices;
public class LoginService(ILoginRepository loginRepository) : ILoginServices
{
    private readonly ILoginRepository _loginRepository = loginRepository;

    public async Task<ApiResponse<string>> UpdatePasswordAsync(string email, string newPass)
    {
        var loginInfos = await _loginRepository.GetLoginInfos(email);

        if (loginInfos is null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        loginInfos.Password = newPass;

        await _loginRepository.UpdatePassword(loginInfos);

        return ApiResponse<string>.Success("Password changed with successfully");
    }

    public async Task<ApiResponse<string>> UpdateEmailAsync(string email, string newEmail)
    {
        var loginInfos = await _loginRepository.GetLoginInfos(email);

        if (loginInfos is null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        loginInfos.Email = newEmail;

        await _loginRepository.UpdateEmail(loginInfos);

        return ApiResponse<string>.Success("Email changed with successfully");
    }

    public async Task<ApiResponse<LoginInfos>> GetLoginsInfosAsync(string email)
    {
        var loginInfos = await _loginRepository.GetLoginInfos(email);

        if (loginInfos is null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        return ApiResponse<LoginInfos>.Success(loginInfos);
    }
}