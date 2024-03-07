using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Login.Dtos;

namespace Project.ECommerce.API.Users.src.Models.Users.Dtos;
public class UserPostDto
{
    public string Fullname { get; set; }

    public string Document { get; set; }

    public string Cellphone { get; set; }

    public IList<AddressDto> Address { get; set; }

    public LoginInfosDto LoginInfos { get; set; }
}