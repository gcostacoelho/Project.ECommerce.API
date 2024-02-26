using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Users.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Services.Interfaces;
public interface IUserServices
{
    ApiResponse<List<User>> GetAllUsers();
    Task<ApiResponse<User>> GetUser(string userId);
    Task<ApiResponse<User>> CreateUser(UserPostDto user);
    Task<ApiResponse<User>> UpdateUser(string userId, UserDto userDto);
    Task DeleteUser(string userId);
}