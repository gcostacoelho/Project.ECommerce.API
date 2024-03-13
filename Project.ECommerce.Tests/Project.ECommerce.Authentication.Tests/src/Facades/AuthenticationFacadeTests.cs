using FluentAssertions;
using Moq;
using Project.ECommerce.API.Authentication.src.Facades;
using Project.ECommerce.API.Authentication.src.Models;
using Project.ECommerce.API.Authentication.src.Models.Login;
using Project.ECommerce.API.Authentication.src.Models.Utils;
using Project.ECommerce.API.Authentication.src.Repositories.Interfaces;
using Project.ECommerce.API.Authentication.src.Services.Interfaces;

namespace Project.ECommerce.Tests.Project.ECommerce.Authentication.Tests.src.Facades;
public class AuthenticationFacadeTests
{
    private readonly Mock<ILoginRepository> loginRepositoryMocked = new Mock<ILoginRepository>();
    private readonly Mock<ITokenServices> tokenServicesMocked = new Mock<ITokenServices>();
    private readonly AuthenticationFacade _authenticationFacade;

    public AuthenticationFacadeTests()
    {
        _authenticationFacade = new AuthenticationFacade(loginRepositoryMocked.Object, tokenServicesMocked.Object);
    }

    [Fact]
    public async void AuthenticationFacade_CreateTokenAsync_ReturnApiResponseString()
    {
        // Arrange
        var loginInfos = LoginInfosMocked();

        loginRepositoryMocked.Setup(x => x.GetLoginInfos(loginInfos.Email)).ReturnsAsync(loginInfos);
        tokenServicesMocked.Setup(x => x.CreateTokenAsync(loginInfos.Id.ToString(), loginInfos.Email)).Returns("Token");

        // Act

        var result = await _authenticationFacade.CreateTokenAsync(loginInfos.Email, loginInfos.Password);

        // Assert
        result.Should().BeOfType<ApiResponse<string>>();
    }

    [Fact]
    public async void AuthenticationFacade_CreateTokenAsync_ReturnApiExceptionUserNotFound()
    {
        // Arrange
        var loginInfos = LoginInfosMocked();

        // Act

        Func<Task> result = async () => await _authenticationFacade.CreateTokenAsync(loginInfos.Email, loginInfos.Password);

        // Assert
        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void AuthenticationFacade_CreateTokenAsync_ReturnApiExceptionUnauthorized()
    {
        // Arrange
        var loginInfos = LoginInfosMocked();

        loginRepositoryMocked.Setup(x => x.GetLoginInfos(loginInfos.Email)).ReturnsAsync(loginInfos);

        // Act

        Func<Task> result = async () => await _authenticationFacade.CreateTokenAsync(loginInfos.Email, "Test123");

        // Assert
        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.UNAUTHORIZED);
    }

    [Fact]
    public void AuthenticationFacade_ValidateToken_ReturnApiResponseString()
    {
        // Arrange
        tokenServicesMocked.Setup(x => x.ValidateTokenAsync("Token")).Returns(true);

        // Act
        var result = _authenticationFacade.ValidateToken("Token");

        // Assert
        result.Should().BeOfType<ApiResponse<string>>();
        result.Data.Should().Be(Constants.TOKEN_VALID);
    }

    [Fact]
    public void AuthenticationFacade_ValidateToken_ReturnApiExceptionTokenNullOrEmpty()
    {
        // Arrange

        // Act
        var result = () => _authenticationFacade.ValidateToken(null);

        // Assert
        result.Should().Throw<ApiException>().WithMessage(Constants.UNAUTHORIZED);
    }

    [Fact]
    public void AuthenticationFacade_ValidateToken_ReturnApiExceptionTokenNotValid()
    {
        // Arrange

        // Act
        var result = () => _authenticationFacade.ValidateToken("Token");

        // Assert
        result.Should().Throw<ApiException>().WithMessage(Constants.UNAUTHORIZED);
    }

    private static LoginInfos LoginInfosMocked()
    {
        return new LoginInfos
        {
            Email = "email@test.com",
            Password = "Password"
        };
    }
}