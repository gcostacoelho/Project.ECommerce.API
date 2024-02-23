using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;

namespace Project.ECommerce.API.Users.src.Repositories.AddressRepository;
public class AddressRepository(AppDbContext appDbContext) : IAddressRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<bool> UpdateAddress(Address address)
    {
        _appDbContext.Entry(address).State = EntityState.Modified;

        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAddress(Address address)
    {
        _appDbContext.Adresses.Remove(address);

        await _appDbContext.SaveChangesAsync();
    }
}