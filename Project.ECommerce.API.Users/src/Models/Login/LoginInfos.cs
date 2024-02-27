using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Models.Login;

public class LoginInfos : BaseModel
{
    public string Password { get; set; }

    public string UserId { get; set; }
}