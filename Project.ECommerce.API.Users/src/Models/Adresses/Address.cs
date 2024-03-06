using System.Text.Json.Serialization;
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

    public Guid UserId { get; set; }

    [JsonIgnore]
    public User User {get; set;}

}