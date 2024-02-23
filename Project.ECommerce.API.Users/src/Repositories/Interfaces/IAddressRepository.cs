using Project.ECommerce.API.Users.src.Models.Adresses;

namespace Project.ECommerce.API.Users.src.Repositories.Interfaces;
public interface IAddressRepository
{
    Task<bool> UpdateAddress(Address address);
    Task DeleteAddress(Address address);
}