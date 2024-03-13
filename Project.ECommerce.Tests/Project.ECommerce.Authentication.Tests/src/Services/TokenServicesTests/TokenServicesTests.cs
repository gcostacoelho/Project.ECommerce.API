using FluentAssertions;
using Moq;
using Project.ECommerce.API.Authentication.src.Interfaces;
using Project.ECommerce.API.Authentication.src.Services.Token;

namespace Project.ECommerce.Tests.Project.ECommerce.Authentication.Tests.src.Services.TokenServicesTests;
public class TokenServicesTests
{
    private const string SECRET_FOR_TESTS = "wHSI8sAvf5UugpEUOmmC1qg2rFkLXbQbpxzibrkK3oJWGQJku67ADsq5Vdn1wgcr";
    private readonly Mock<IAppSettings> appSettingsMocked = new Mock<IAppSettings>();

    private readonly TokenServices _tokenServices;

    public TokenServicesTests()
    {
        _tokenServices = new TokenServices(appSettingsMocked.Object);
    }

    [Fact]
    public void TokenServices_CreateTokenAsync_ReturnString()
    {
        // Arrange

        appSettingsMocked.Setup(x => x.Secret).Returns(SECRET_FOR_TESTS);

        // Act

        var result = _tokenServices.CreateTokenAsync("identity", "email@test.com");

        // Assert

        result.Should().BeOfType<string>();
    }

    [Fact]
    public void TokenServices_ValidateTokenAsync_ReturnTrue()
    {
        // Arrange
        var token = JsonWebTokenForTests();

        appSettingsMocked.Setup(x => x.Secret).Returns(SECRET_FOR_TESTS);

        // Act

        var result = _tokenServices.ValidateTokenAsync(token);

        // Assert

        result.Should().BeTrue();
    }

    [Fact]
    public void TokenServices_ValidateTokenAsync_ReturnNull()
    {
        // Arrange
        appSettingsMocked.Setup(x => x.Secret).Returns(SECRET_FOR_TESTS);

        // Act

        var result = _tokenServices.ValidateTokenAsync("token");

        // Assert

        result.Should().BeNull();
    }

    private string JsonWebTokenForTests()
    {
        appSettingsMocked.Setup(x => x.Secret).Returns(SECRET_FOR_TESTS);

        return _tokenServices.CreateTokenAsync("identity", "email@test.com");
    }
}