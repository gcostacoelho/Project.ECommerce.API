using Newtonsoft.Json;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Utils;

namespace Project.ECommerce.API.Users.src.Models.Adresses;
public class Address : BaseModel
{
    public string PostalCode { get; set; }

    public string Street { get; set; }

    public int Number { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Complement { get; set; }

    public string UserId { get; set; }

}