using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Models.Login;

[Index(nameof(Email), IsUnique = true)]
public class LoginInfos : BaseModel
{
    public string Email { get; set; }
    public string Password { get; set; }

    public Guid UserId { get; set; }

    [JsonIgnore]
    public User User { get; set; }
}