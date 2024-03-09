using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Services.Interfaces;
public interface ILoginServices
{
    Task<ApiResponse<string>> UpdatePasswordAsync(string email, string newPass);

    Task<ApiResponse<string>> UpdateEmailAsync(string email, string newEmail);

    Task<ApiResponse<LoginInfos>> GetLoginsInfosAsync(string  email);
}