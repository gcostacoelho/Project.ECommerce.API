using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Repositories.LoginRepository;

namespace Project.ECommerce.Tests.Project.ECommerce.Users.Tests.src.Repositories.LoginRepositoryTests;
public class LoginRepositoryTests
{

    [Fact]
    public async void LoginRepository_UpdatePassword_ReturnTrue()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();

        var user = dbContext.Users.ToList()[0];

        var loginInfo = user.LoginInfos;

        var loginRepository = new LoginRepository(dbContext);

        loginInfo.Password = "test1234";

        // Act
        var result = await loginRepository.UpdatePassword(loginInfo);

        // Assert
        result.Should().BeTrue();
    }

    private async Task<AppDbContext> GetAppDbContextAsync()
    {
        var user = MockedUser();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new AppDbContext(options);

        databaseContext.Database.EnsureCreated();

        if (await databaseContext.Users.CountAsync() <= 0)
        {
            databaseContext.Users.Add(user);
            databaseContext.Logins.Add(user.LoginInfos);

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
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