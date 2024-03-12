using Project.ECommerce.API.Authentication.src.Models.Login;

namespace Project.ECommerce.API.Authentication.src.Repositories.Interfaces;
public interface ILoginRepository
{
    Task<LoginInfos> GetLoginInfos(string email);
}