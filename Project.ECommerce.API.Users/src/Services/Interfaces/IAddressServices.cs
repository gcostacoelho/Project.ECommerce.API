using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Services.Interfaces;
public interface IAddressServices
{
    Task<ApiResponse<string>> UpdateAddressAsync(string userId, Address address);

    Task DeleteAddressAsync(string userId, string addressId);
}