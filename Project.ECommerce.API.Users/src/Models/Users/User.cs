using Microsoft.EntityFrameworkCore;

using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Models.Users;

[Index(nameof(Email), IsUnique = true)]
public class User : BaseModel
{
    public string Fullname { get; set; }

    public string Email { get; set; }

    public string Document { get; set; }

    public string Cellphone { get; set; }

    public IList<Address> Address { get; set; }

    public LoginInfos LoginInfos { get; set; }
}