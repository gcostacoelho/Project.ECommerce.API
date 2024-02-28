using System.Text.Json.Serialization;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Models.Login;

public class LoginInfos : BaseModel
{
    public string Password { get; set; }

    public Guid UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }
}