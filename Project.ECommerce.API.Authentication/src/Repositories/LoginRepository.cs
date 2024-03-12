using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Authentication.src.Database;
using Project.ECommerce.API.Authentication.src.Models.Login;
using Project.ECommerce.API.Authentication.src.Repositories.Interfaces;

namespace Project.ECommerce.API.Authentication.src.Repositories;
public class LoginRepository(AppDbContext appDbContext) : ILoginRepository
{

    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<LoginInfos> GetLoginInfos(string email)
    {
        return await _appDbContext.Logins.FirstOrDefaultAsync(x => x.Email == email);
    }
}