using FluentAssertions;
using Microsoft.EntityFrameworkCore;

using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Repositories.UserRepository;

namespace Project.ECommerce.Tests.Project.ECommerce.Users.Tests.src.Repositories.UserRepositoryTests;

public class UserRepositoryTests
{
    [Fact]
    public async void UserRepository_GetUser_ReturnUser()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();

        var user = dbContext.Users.ToList()[0];
        var userId = user.Id.ToString();

        var userRepository = new UserRepository(dbContext);

        // Act

        var result = await userRepository.GetUser(userId);

        // Assert

        result.Should().NotBeNull();
        result.Should().BeOfType<User>();
        result.Should().BeEquivalentTo(user);
    }

    [Fact]
    public async void UserRepository_GetUser_ReturnNull()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userId = Guid.NewGuid().ToString();

        var userRepository = new UserRepository(dbContext);

        // Act

        var result = await userRepository.GetUser(userId);

        // Assert

        result.Should().BeNull();
    }

    [Fact]
    public async void UserRepository_CreateUser_ReturnVoid()
    {
        // Arrange

        var user = MockedUser();

        user.Email = "email@teste.com";

        var dbContext = await GetAppDbContextAsync();
        var userRepository = new UserRepository(dbContext);

        // Act

        Func<Task> result = async () => await userRepository.CreateUser(user);

        // Assert

        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async void UserRepository_CreateUser_ReturnException()
    {
        // Arrange
        var user = MockedUser();

        var dbContext = await GetAppDbContextAsync();
        var userFromDBInMemory = dbContext.Users.ToList()[0];

        user.Id = userFromDBInMemory.Id;

        var userRepository = new UserRepository(dbContext);

        // Act

        Func<Task> result = async () => await userRepository.CreateUser(user);

        // Assert

        await result.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async void UserRepository_GetAllUsers_ReturnListUsers()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userRepository = new UserRepository(dbContext);

        // Act

        var result = userRepository.GetAllUsers();

        // Assert

        result.Should().NotBeEmpty();
        result.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async void UserRepository_GetAllUsers_ReturnEmpty()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();
        var userFromDBInMemory = dbContext.Users.ToList()[0];

        dbContext.Users.Remove(userFromDBInMemory);
        dbContext.SaveChanges();

        var userRepository = new UserRepository(dbContext);

        // Act

        var result = userRepository.GetAllUsers();

        // Assert

        result.Should().BeEmpty();
        result.Should().BeOfType<List<User>>();
    }

    [Fact]
    public async void UserRepository_UpdateUser_ReturnBool()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();

        var userFromDBInMemory = dbContext.Users.ToList()[0];

        var userRepository = new UserRepository(dbContext);

        // Act 
        var result = await userRepository.UpdateUser(userFromDBInMemory);

        // Assert
        result.Should().BeTrue();
    }


    [Fact]
    public async void UserRepository_DeleteUser_ReturnVoid()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userFromDBInMemory = dbContext.Users.ToList()[0];

        var userRepository = new UserRepository(dbContext);

        // Act

        Func<Task> result = async () => await userRepository.DeleteUser(userFromDBInMemory);

        // Assert

        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async void UserRepository_DeleteUser_ReturnException()
    {
        // Arrange

        var dbContext = await GetAppDbContextAsync();

        var userFromDBInMemory = dbContext.Users.ToList()[0];

        userFromDBInMemory.Id = Guid.NewGuid();

        var userRepository = new UserRepository(dbContext);

        // Act

        Func<Task> result = async () => await userRepository.DeleteUser(userFromDBInMemory);

        // Assert

        await result.Should().ThrowAsync<Exception>();
    }

    private async Task<AppDbContext> GetAppDbContextAsync()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var databaseContext = new AppDbContext(options);

        databaseContext.Database.EnsureCreated();

        if (await databaseContext.Users.CountAsync() <= 0)
        {
            databaseContext.Users.Add(MockedUser());

            await databaseContext.SaveChangesAsync();
        }

        return databaseContext;
    }

    private static User MockedUser()
    {
        var loginInfo = new LoginInfos { Password = "test1234" };
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
            Email = "Email",
            Cellphone = "Cellphone",
            Document = "Document",
            LoginInfos = loginInfo,
            Address = address
        };

    }
}