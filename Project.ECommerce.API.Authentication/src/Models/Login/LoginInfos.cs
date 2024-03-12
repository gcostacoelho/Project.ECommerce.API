using Project.ECommerce.API.Authentication.src.Models.Utils;

namespace Project.ECommerce.API.Authentication.src.Models.Login;
public class LoginInfos : BaseModel
{
    public string Email { get; set; }
    
    public string Password { get; set; }

    public Guid UserId { get; set; }
}