using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Services.Interfaces;
public interface ILoginServices
{
    Task<ApiResponse<string>> UpdatePasswordAsync(string userId, string newPass);
}