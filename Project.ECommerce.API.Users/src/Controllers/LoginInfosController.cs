using Microsoft.AspNetCore.Mvc;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Controllers;

[ApiController]
[Route("api/login/[controller]")]
public class LoginInfosController(ILoginServices loginServices) : ControllerBase
{
    private readonly ILoginServices _loginServices = loginServices;

    [HttpPatch]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdatePasswordAsync(string userId, string newPass)
    {
        var response = await _loginServices.UpdatePasswordAsync(userId, newPass);

        return Ok(response);
    }
}