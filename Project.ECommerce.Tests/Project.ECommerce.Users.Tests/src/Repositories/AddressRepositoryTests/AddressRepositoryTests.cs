using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Repositories.AddressRepository;

namespace Project.ECommerce.Tests.Project.ECommerce.Users.Tests.src.Repositories.AddressRepositoryTests;
public class AddressRepositoryTests
{
    [Fact]
    public async void AddressRepository_UpdateAddress_ReturnTrue()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();
        var user = dbContext.Users.ToList()[0];

        var addressRepository = new AddressRepository(dbContext);

        // Act

        var result = await addressRepository.UpdateAddress(user.Address[0]);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async void AddressRepository_AddNewAddress_ReturnVoid()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();
        var user = dbContext.Users.ToList()[0];

        var newAddress = new Address
        {
            Street = "Street1",
            Number = 1,
            City = "City 1",
            Complement = "Complement",
            Country = "Country",
            PostalCode = "PostalCode",
            UserId = user.Id,
            User = user
        };


        var addressRepository = new AddressRepository(dbContext);

        // Act

        Func<Task> result = async () => await addressRepository.AddNewAddress(newAddress);

        // Assert

        await result.Should().NotThrowAsync();
    }


    [Fact]
    public async void AddressRepository_AddNewAddress_ReturnException()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();
        var user = dbContext.Users.ToList()[0];

        var newAddress = new Address
        {
            Id = user.Address[0].Id,
            Street = "Street1",
            Number = 1,
            City = "City 1",
            Complement = "Complement",
            Country = "Country",
            PostalCode = "PostalCode",
            UserId = user.Id,
            User = user
        };


        var addressRepository = new AddressRepository(dbContext);

        // Act

        Func<Task> result = async () => await addressRepository.AddNewAddress(newAddress);

        // Assert

        await result.Should().ThrowAsync<Exception>();
    }


    [Fact]
    public async void AddressRepository_DeleteAddress_ReturnVoid()
    {
        // Arrange
        var dbContext = await GetAppDbContextAsync();
        var user = dbContext.Users.ToList()[0];

        var addressRepository = new AddressRepository(dbContext);

        // Act

        Func<Task> result = async () => await addressRepository.DeleteAddress(user.Address[0]);

        // Assert

        await result.Should().NotThrowAsync();
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
            databaseContext.Adresses.Add(user.Address[0]);

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