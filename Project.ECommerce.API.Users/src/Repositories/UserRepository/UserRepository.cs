using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;

namespace Project.ECommerce.API.Users.src.Repositories.UserRepository;
public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task CreateUser(User user)
    {
        try
        {
            _appDbContext.Users.Add(user);

            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteUser(User user)
    {
        try
        {
            _appDbContext.Users.Remove(user);

            await _appDbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IList<User> GetAllUsers()
    {
        return _appDbContext.Users
            .Include(u => u.Address)
            .Include(u => u.LoginInfos)
            .ToList();
    }

    public async Task<User> GetUser(string userId)
    {
        try
        {
            return await _appDbContext.Users
                .Include(u => u.Address)
                .Include(u => u.LoginInfos)
                .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> UpdateUser(User userEntity)
    {
        try
        {
            _appDbContext.Entry(userEntity).State = EntityState.Modified;

            await _appDbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}