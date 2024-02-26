using System.Net;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Login.Dtos;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Users.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Services.UserServices;
public class UserService(IUserRepository userRepository) : IUserServices
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<ApiResponse<User>> CreateUser(UserPostDto userBody)
    {
        var loginInfoModel = MountLoginInfos(userBody.LoginInfos);
        var addressModel = MountAddress(userBody.Address);

        var user = new User
        {
            Fullname = userBody.Fullname,
            Email = userBody.Email,
            Cellphone = userBody.Cellphone,
            Document = userBody.Document,
            LoginInfos = loginInfoModel,
            Address = addressModel
        };

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

    private static LoginInfos MountLoginInfos(LoginInfosDto loginInfosDto)
    {
        return new LoginInfos
        {
            Username = loginInfosDto.Username,
            Password = loginInfosDto.Password
        };
    }

    private static List<Address> MountAddress(IList<AddressDto> addressDto)
    {
        var adresses = new List<Address>();

        foreach (var item in addressDto)
        {
            var address = new Address
            {
                PostalCode = item.PostalCode,
                Street = item.Street,
                Number = item.Number,
                City = item.City,
                Country = item.Country,
                Complement = item.Complement
            };

            adresses.Add(address);
        }

        return adresses;
    }
}