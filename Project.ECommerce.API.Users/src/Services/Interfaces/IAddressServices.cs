using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Services.Interfaces;
public interface IAddressServices
{
    Task<ApiResponse<string>> UpdateAddressAsync(string userId, string addressId, AddressDto address);

    Task<ApiResponse<string>> NewAddressAsync(string userId, AddressDto address);

    Task DeleteAddressAsync(string userId, string addressId);
}