using Project.ECommerce.API.Users.src.Models.Users;

namespace Project.ECommerce.API.Users.src.Repositories.Interfaces;
public interface IUserRepository
{
    IList<User> GetAllUsers();
    Task<User> GetUser(string userId);
    Task CreateUser(User user);
    Task<bool> UpdateUser(User userEntity);
    Task DeleteUser(User user);
}