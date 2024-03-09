using FluentAssertions;
using Moq;

using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.AddressServices;

namespace Project.ECommerce.Tests.Project.ECommerce.Users.Tests.src.Services.AddressServicesTests;
public class AddressServiceTests
{
    private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();
    private readonly Mock<IAddressRepository> _addressRepository = new Mock<IAddressRepository>();

    private readonly AddressService _addressService;

    public AddressServiceTests()
    {
        _addressService = new AddressService(_addressRepository.Object, _userRepository.Object);
    }

    [Fact]
    public async void AddressService_DeleteAddressAsync_ReturnVoid()
    {
        // Arrange       
        var user = MockedUser();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        // Act

        Func<Task> result = async () => await _addressService.DeleteAddressAsync(user.Id.ToString(), user.Address[0].Id.ToString());

        // Assert

        await result.Should().NotThrowAsync();
    }

    [Fact]
    public async void AddressService_DeleteAddressAsync_ReturnApiExceptionUserNotFound()
    {
        // Arrange       
        var userId = Guid.NewGuid().ToString();
        var addressId = Guid.NewGuid().ToString();

        // Act

        Func<Task> result = async () => await _addressService.DeleteAddressAsync(userId, addressId);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void AddressService_DeleteAddressAsync_ReturnApiExceptionAddressNotFound()
    {
        // Arrange       
        var user = MockedUser();
        var addressId = Guid.NewGuid().ToString();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        // Act

        Func<Task> result = async () => await _addressService.DeleteAddressAsync(user.Id.ToString(), addressId);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.ADDRESS_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void AddressService_UpdateAddressAsync_ReturnsApiResponseString()
    {
        // Arrange

        var user = MockedUser();
        var address = MockedAddress();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        // Act

        var result = await _addressService.UpdateAddressAsync(user.Id.ToString(), user.Address[0].Id.ToString(), address);

        // Assert

        result.Should().BeOfType<ApiResponse<string>>();
        result.Data.Should().Be("Address updated with successfully");
    }

    [Fact]
    public async void AddressService_UpdateAddressAsync_ReturnsApiExceptionUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var addressId = Guid.NewGuid().ToString();
        var address = MockedAddress();

        // Act

        Func<Task> result = async () => await _addressService.UpdateAddressAsync(userId, addressId, address);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
    }

    [Fact]
    public async void AddressService_UpdateAddressAsync_ReturnsApiExceptionAddressNotFound()
    {
        // Arrange
        var user = MockedUser();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        var addressId = Guid.NewGuid().ToString();
        var address = MockedAddress();

        // Act

        Func<Task> result = async () => await _addressService.UpdateAddressAsync(user.Id.ToString(), addressId, address);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.ADDRESS_NOT_FOUND_MESSAGE);
    }


    [Fact]
    public async void AddressService_NewAddressAsync_ReturnsApiResponseString()
    {
        // Arrange

        var user = MockedUser();
        var address = MockedAddress();

        _userRepository.Setup(x => x.GetUser(user.Id.ToString())).ReturnsAsync(user);

        // Act

        var result = await _addressService.NewAddressAsync(user.Id.ToString(), address);

        // Assert

        result.Should().BeOfType<ApiResponse<string>>();
        result.Data.Should().Be("Address added successfully");
    }


    [Fact]
    public async void AddressService_NewAddressAsync_ReturnsApiExceptionUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid().ToString();
        var address = MockedAddress();

        // Act

        Func<Task> result = async () => await _addressService.NewAddressAsync(userId, address);

        // Assert

        await result.Should().ThrowAsync<ApiException>().WithMessage(Constants.USER_NOT_FOUND_MESSAGE);
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

    private static AddressDto MockedAddress()
    {
        return new AddressDto
        {
            PostalCode = "PostalCode",
            Street = "Street",
            Number = 0,
            City = "City",
            Country = "Country",
            Complement = "Complement"
        };
    }
}