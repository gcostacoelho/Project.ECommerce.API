namespace Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
public class AddressDto
{
    public string PostalCode { get; set; }

    public string Street { get; set; }

    public int Number { get; set; }

    public string City { get; set; }

    public string Country { get; set; }

    public string Complement { get; set; }
}