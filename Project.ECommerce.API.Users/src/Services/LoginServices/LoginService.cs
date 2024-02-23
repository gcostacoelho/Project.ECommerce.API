using System.Net;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Services.LoginServices;
public class LoginService(ILoginRepository loginRepository, IUserRepository userRepository) : ILoginServices
{
    private readonly ILoginRepository _loginRepository = loginRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ApiResponse<string>> UpdatePasswordAsync(string userId, string newPass)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        var loginInfos = user.LoginInfos;

        loginInfos.Password = newPass;

        await _loginRepository.UpdatePassword(loginInfos);

        return ApiResponse<string>.Success("Senha alterada com sucesso");   
    }
}