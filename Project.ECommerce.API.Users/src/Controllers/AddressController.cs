using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.ECommerce.API.Users.src.Models.Adresses;
using Project.ECommerce.API.Users.src.Models.Adresses.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AddressController(IAddressServices addressServices) : ControllerBase
{
    private readonly IAddressServices _addressServices = addressServices;

    /// <summary>
    /// Update a user address in database 
    /// </summary>
    /// <param name="userId">Unique identifier from user</param>
    /// <param name="addressId">Unique identifier from Address</param>
    /// <param name="address">Body contain the infos to updated</param>

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAddressAsync([FromHeader, Required] string userId, string addressId, [FromBody, Required] AddressDto address)
    {
        var response = await _addressServices.UpdateAddressAsync(userId, addressId, address);

        return Ok(response);
    }

    /// <summary>
    /// Delete a Address in Database from user
    /// </summary>
    /// <param name="userId">Unique identifier from user</param>
    /// <param name="addressId">Unique identifier from Address</param>

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAddressAsync([FromHeader, Required] string userId, string addressId)
    {
        await _addressServices.DeleteAddressAsync(userId, addressId);

        return NoContent();
    }

    /// <summary>
    /// Add a new address infos
    /// </summary>
    /// <param name="userId">Unique identifier from user</param>
    /// <param name="address">Body contain the infos</param>

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> NewAddressAsync([FromHeader, Required] string userId, [FromBody, Required] AddressDto address)
    {
        var response = await _addressServices.NewAddressAsync(userId, address);

        return Ok(response);
    }
}