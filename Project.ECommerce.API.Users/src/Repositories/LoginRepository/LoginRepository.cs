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
}