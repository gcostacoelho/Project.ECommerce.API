using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Models.Login;

[Index(nameof(Username), IsUnique = true)]
public class LoginInfos : BaseModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}