using System.Net;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Services.AddressServices;
public class AddressService(IAddressRepository addressRepository, IUserRepository userRepository) : IAddressServices
{
    private readonly IAddressRepository _addressRepository = addressRepository;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task DeleteAddressAsync(string userId, string addressId)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        var userAddressSelected = user.Address.ToList().Find(x => x.Id == Guid.Parse(addressId));

        await _addressRepository.DeleteAddress(userAddressSelected);
    }

    public async Task<ApiResponse<string>> UpdateAddressAsync(string userId, string addressId, AddressDto address)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        var userAddressSelected = user.Address.ToList().Find(x => x.Id == Guid.Parse(addressId)) ?? throw new ApiException("Address not found", HttpStatusCode.BadRequest);

        userAddressSelected.Street = address.Street;
        userAddressSelected.Number = address.Number;
        userAddressSelected.City = address.City;
        userAddressSelected.Complement = address.Complement;
        userAddressSelected.Country = address.Country;
        userAddressSelected.PostalCode = address.PostalCode;

        await _addressRepository.UpdateAddress(userAddressSelected);

        return ApiResponse<string>.Success("address updated with successfully");
    }

    public async Task<ApiResponse<string>> NewAddressAsync(string userId, AddressDto address)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        var newAddress = new Address
        {
            Street = address.Street,
            Number = address.Number,
            City = address.City,
            Complement = address.Complement,
            Country = address.Country,
            PostalCode = address.PostalCode,
            UserId = user.Id,
            User = user
        };

        await _addressRepository.AddNewAddress(newAddress);

        return ApiResponse<string>.Success("Address added successfully");
    }
}