using System.Net;
using Project.ECommerce.API.Users.src.Models;
using Project.ECommerce.API.Users.src.Models.Adresses;
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

        var userAddressSelected = user.Address.ToList().Find(x => x.Id == addressId);

        await _addressRepository.DeleteAddress(userAddressSelected);
    }

    public async Task<ApiResponse<string>> UpdateAddressAsync(string userId, Address address)
    {
        var user = await _userRepository.GetUser(userId);

        if (user == null)
        {
            throw new ApiException(Constants.USER_NOT_FOUND_MESSAGE, HttpStatusCode.BadRequest);
        }

        var userAddressSelected = user.Address.ToList().Find(x => x.Id == address.Id) ?? throw new ApiException("Address not found", HttpStatusCode.BadRequest);
        
        await _addressRepository.UpdateAddress(address);

        return ApiResponse<string>.Success("address updated with successfully");       
    }
}