using System.Net;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Users.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Services.UserServices;
public class UserService(IUserRepository userRepository) : IUserServices
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ApiResponse<User>> CreateUser(User user)
    {
        await _userRepository.CreateUser(user);

        return ApiResponse<User>.Success(user);
    }

    public async Task DeleteUser(string userId)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        await _userRepository.DeleteUser(user);
    }

    public ApiResponse<List<User>> GetAllUsers()
    {
        var users = _userRepository.GetAllUsers().ToList();

        return ApiResponse<List<User>>.Success(users);
    }

    public async Task<ApiResponse<User>> GetUser(string userId)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        return ApiResponse<User>.Success(user);
    }

    public async Task<ApiResponse<User>> UpdateUser(string userId, UserDto userDto)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        var userEntity = new User
        {
            Fullname = userDto.Fullname,
            Document = userDto.Document,
            Cellphone = userDto.Cellphone,
            Email = userDto.Email,
            LoginInfos = user.LoginInfos,
            Address = user.Address
        };

        await _userRepository.UpdateUser(userEntity);

        var userUpdated = await _userRepository.GetUser(userId);

        return ApiResponse<User>.Success(userUpdated);
    }
}