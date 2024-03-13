using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Authentication.src.Database;
using Project.ECommerce.API.Authentication.src.Models.Login;
using Project.ECommerce.API.Authentication.src.Repositories;

namespace Project.ECommerce.Tests.Project.ECommerce.Authentication.Tests.src.Repositories.LoginRepositoryTests;
public class LoginRepositoryTests
{
    [Fact]
    public async void LoginRepository_GetLoginInfos_ReturnLoginInfos()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var loginRepository = new LoginRepository(dbContext);

        // Act

        var result = await loginRepository.GetLoginInfos("Email");

        // Assert

        result.Should().NotBeNull();
    }

    [Fact]
    public async void LoginRepository_GetLoginInfos_ReturnNull()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var loginRepository = new LoginRepository(dbContext);

        // Act

        var result = await loginRepository.GetLoginInfos("email@test.com");

        // Assert

        result.Should().BeNull();
    }

    private async Task<AppDbContext> GetAppDbContextAsync()
    {
        var loginInfo = new LoginInfos { Email = "Email", Password = "Password" };

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new AppDbContext(options);

        databaseContext.Database.EnsureCreated();

        if (await databaseContext.Logins.CountAsync() <= 0)
        {
            databaseContext.Logins.Add(loginInfo);

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }
}