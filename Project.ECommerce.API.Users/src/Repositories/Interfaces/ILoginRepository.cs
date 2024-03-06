using Project.ECommerce.API.Users.src.Models.Login;

namespace Project.ECommerce.API.Users.src.Repositories.Interfaces;
public interface ILoginRepository
{
    Task<bool> UpdatePassword(LoginInfos loginInfos);
}