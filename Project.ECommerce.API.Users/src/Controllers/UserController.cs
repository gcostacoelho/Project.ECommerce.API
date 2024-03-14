using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project.ECommerce.API.Users.src.Models.Users;
using Project.ECommerce.API.Users.src.Models.Users.Dtos;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserServices userServices) : ControllerBase
{
    private readonly IUserServices _userServices = userServices;


    /// <summary>
    /// Get a user data
    /// </summary>
    /// <param name="userId">Unique identifier from user</param>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserAsync([FromHeader, Required] string userId)
    {
        var response = await _userServices.GetUser(userId);

        return Ok(response);
    }

    /// <summary>
    /// Get all users in database
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(ApiResponse<List<User>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllUserAsync()
    {
        var response = _userServices.GetAllUsers();

        return Ok(response);
    }

    /// <summary>
    /// Create a new user in database
    /// </summary>
    /// <param name="user">user informations</param>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUserAsync([FromBody, Required] UserPostDto user)
    {
        var response = await _userServices.CreateUser(user);

        return Ok(response);
    }

    /// <summary>
    /// Update a user data
    /// </summary>
    /// <param name="userId">Unique identifier</param>
    /// <param name="user">User data to update</param>
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAddressAsync([FromHeader, Required] string userId, [FromBody, Required] UserDto user)
    {
        var response = await _userServices.UpdateUser(userId, user);

        return Ok(response);
    }


    /// <summary>
    /// Delete a user with yours infos from database
    /// </summary>
    /// <param name="userId">Unique identifier</param>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteAddressAsync([FromHeader, Required] string userId)
    {
        await _userServices.DeleteUser(userId);

        return NoContent();
    }
}