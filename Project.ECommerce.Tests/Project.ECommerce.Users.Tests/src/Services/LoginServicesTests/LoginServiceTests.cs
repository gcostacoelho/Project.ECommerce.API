using FluentAssertions;
using Moq;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.LoginServices;

namespace Project.ECommerce.Tests.Project.ECommerce.Users.Tests.src.Services.LoginServicesTests;
public class LoginServiceTests
{
    private readonly Mock<ILoginRepository> _loginRepository = new Mock<ILoginRepository>();

    private readonly LoginService _loginService;

    public LoginServiceTests()
    {
        _loginService = new LoginService(_loginRepository.Object);
    }

    [Fact]
    public async void LoginService_UpdatePasswordAsync_ReturnApiResponseString()
    {
        // Arrange
        var user = MockedUser();
        var userEmail = user.LoginInfos.Email;

        _loginRepository.Setup(x => x.GetLoginInfos(userEmail)).ReturnsAsync(user.LoginInfos);
        _loginRepository.Setup(x => x.UpdatePassword(user.LoginInfos)).ReturnsAsync(true);

        // Act

        var result = await _loginService.UpdatePasswordAsync(userEmail, "test1234");

        // Assert

        result.Should().BeOfType<ApiResponse<string>>();
        result.Data.Should().Be("Password changed with successfully");
    }

    [Fact]
    public async void LoginService_UpdatePasswordAsync_ReturnApiException()
    {
        // Arrange

        var id = Guid.NewGuid().ToString();

        // Act

        Func<Task> result = async () => await _loginService.UpdatePasswordAsync(id, "test1234");

        // Assert

        await result.Should()
            .ThrowAsync<ApiException>()
            .WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void LoginService_UpdateEmailAsync_ReturnApiResponseString()
    {
        // Arrange
        var user = MockedUser();
        var userEmail = user.LoginInfos.Email;

        _loginRepository.Setup(x => x.GetLoginInfos(userEmail)).ReturnsAsync(user.LoginInfos);
        _loginRepository.Setup(x => x.UpdateEmail(user.LoginInfos)).ReturnsAsync(true);

        // Act

        var result = await _loginService.UpdateEmailAsync(userEmail, "email1234");

        // Assert

        result.Should().BeOfType<ApiResponse<string>>();
        result.Data.Should().Be("Email changed with successfully");
    }

    [Fact]
    public async void LoginService_UpdateEmailAsync_ReturnApiException()
    {
        // Arrange

        // Act

        Func<Task> result = async () => await _loginService.UpdateEmailAsync("email", "email1235");

        // Assert

        await result.Should()
            .ThrowAsync<ApiException>()
            .WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }


    [Fact]
    public async void LoginService_GetLoginInfosAsync_ReturnApiResponseLoginInfos()
    {
        // Arrange

        var mockUser = MockedUser();

        var userEmail = mockUser.LoginInfos.Email;

        _loginRepository.Setup(x => x.GetLoginInfos(userEmail)).ReturnsAsync(mockUser.LoginInfos);

        // Act

        var result = await _loginService.GetLoginsInfosAsync(userEmail);

        //Assert

        result.Should().BeOfType<ApiResponse<LoginInfos>>();
    }

    [Fact]
    public async void LoginService_GetLoginInfosAsync_ReturnApiException()
    {
        // Arrange

        // Act

        Func<Task> result = async () => await _loginService.GetLoginsInfosAsync("email");

        // Assert

        await result.Should()
            .ThrowAsync<ApiException>()
            .WithMessage(Constants.USER_NOT_FOUND_MESSAGE);;
    }



    private static User MockedUser()
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
}