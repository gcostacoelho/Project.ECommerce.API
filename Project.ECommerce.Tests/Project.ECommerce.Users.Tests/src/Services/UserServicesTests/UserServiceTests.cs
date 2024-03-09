using FluentAssertions;
using Moq;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Login.Dtos;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Users.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.UserServices;

namespace Project.ECommerce.Tests.Project.ECommerce.Users.Tests.src.Services.UserServicesTests;
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userService = new UserService(_userRepository.Object);
    }

    [Fact]
    public async void UserService_CreateUser_ReturnApiResponseUser()
    {
        // Arrange
        var user = MockedUserRequest();

        // Act
        var result = await _userService.CreateUser(user);

        // Assert
        result.Should().BeOfType<ApiResponse<User>>();
    }

    [Fact]
    public async void UserService_DeleteUser_ReturnVoid()
    {
        // Arrange
        var user = MockedUserResponse();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);
        // Act

        Func<Task> result = async () => await _userService.DeleteUser(user.Id.ToString());

        // Assert
        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async void UserService_DeleteUser_ReturnApiExceptionUserNotFound()
    {
        // Arrange

        var userId = Guid.NewGuid().ToString();

        // Act

        Func<Task> result = async () => await _userService.DeleteUser(userId);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public void UserService_GetAllUsers_ReturnApiResponseListFromUsers()
    {

        // Arrange

        var user1 = MockedUserResponse();
        var user2 = MockedUserResponse();

        var users = new List<User>
        {
            user1,
            user2
        };

        _userRepository.Setup(x => x.GetAllUsers()).Returns(users);

        // Act

        var result = _userService.GetAllUsers();

        // Assert

        result.Should().BeOfType<ApiResponse<List<User>>>();
    }

    [Fact]
    public async void UserService_GetUser_ReturnApiResponseUser()
    {
        // Arrange

        var user = MockedUserResponse();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        // Act

        var result = await _userService.GetUser(user.Id.ToString());

        // Assert

        result.Should().BeOfType<ApiResponse<User>>();
    }

    [Fact]
    public async void UserService_GetUser_ReturnApiExceptionUserNotFound()
    {

        var userId = Guid.NewGuid().ToString();

        // Act

        Func<Task> result = async () => await _userService.GetUser(userId);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void UserService_UpdateUser_ReturnApiResponseUser()
    {
        // Assert
        var user = MockedUserResponse();
        var userDtoRequest = MockedUserDtoRequest();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        // Act
        var result = await _userService.UpdateUser(user.Id.ToString(), userDtoRequest);

        // Assert
        result.Should().BeOfType<ApiResponse<User>>();
    }


    [Fact]
    public async void UserService_UpdateUser_ReturnApiExceptionUserNotFound()
    {
        // Arrange

        var userId = Guid.NewGuid().ToString();
        var userDtoRequest = MockedUserDtoRequest();

        // Act

        Func<Task> result = async () => await _userService.UpdateUser(userId, userDtoRequest);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    private static UserPostDto MockedUserRequest()
    {
        var loginInfo = new LoginInfosDto { Email = "Email", Password = "test1234" };
        var address = new List<AddressDto>
        {
            new AddressDto
            {
                PostalCode = "PostalCode",
                Street = "Street",
                Number = 0,
                City = "City",
                Country = "Country",
                Complement = "Complement"
            }
        };

        return new UserPostDto
        {
            Fullname = "Fullname",
            Cellphone = "Cellphone",
            Document = "Document",
            LoginInfos = loginInfo,
            Address = address
        };
    }

    private static User MockedUserResponse()
    {
        var loginInfo = new LoginInfos { Email = "Email", Password = "test1234" };
        var address = new List<Address>
        {
            new Address
            {
                PostalCode = "PostalCode",
                Street = "Street",
                Number = 0,
                City = "City",
                Country = "Country",
                Complement = "Complement"
            }
        };

        return new User
        {
            Id = Guid.NewGuid(),
            Fullname = "Fullname",
            Cellphone = "Cellphone",
            Document = "Document",
            LoginInfos = loginInfo,
            Address = address
        };
    }

    private static UserDto MockedUserDtoRequest()
    {
        return new UserDto
        {
            Fullname = "Fullname",
            Cellphone = "Cellphone",
            Document = "Document"
        };
    }
}