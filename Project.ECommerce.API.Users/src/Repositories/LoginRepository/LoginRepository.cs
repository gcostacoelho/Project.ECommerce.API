using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Users.src.Database;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Repositories.Interfaces;

namespace Project.ECommerce.API.Users.src.Repositories.LoginRepository;
public class LoginRepository(AppDbContext appDbContext) : ILoginRepository
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<bool> UpdatePassword(LoginInfos loginInfos)
    {
        _appDbContext.Entry(loginInfos).Property(x => x.Password).IsModified = true;

        await _appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<LoginInfos> GetLoginInfos(string email)
    {
        return await _appDbContext.Logins
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task<bool> UpdateEmail(LoginInfos loginInfos)
    {
        _appDbContext.Entry(loginInfos).Property(x => x.Email).IsModified = true;

        await _appDbContext.SaveChangesAsync();

        return true;
    }
}