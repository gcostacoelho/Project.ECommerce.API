using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;

namespace Project.ECommerce.API.Users.src.Repositories.Interfaces;
public interface IAddressRepository
{
    Task<bool> UpdateAddress(Address address);
    Task AddNewAddress(Address address);
    Task DeleteAddress(Address address);
}