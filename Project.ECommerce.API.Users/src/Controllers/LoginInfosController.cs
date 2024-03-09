using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project.ECommerce.API.Users.src.Models.Login;
using Project.ECommerce.API.Users.src.Models.Utils;
using Project.ECommerce.API.Users.src.Services.Interfaces;

namespace Project.ECommerce.API.Users.src.Controllers;

[ApiController]
[Route("api/login/[controller]")]
public class LoginInfosController(ILoginServices loginServices) : ControllerBase
{
    private readonly ILoginServices _loginServices = loginServices;

    [HttpPatch("pass")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdatePasswordAsync([FromHeader, Required] string email, string newPass)
    {
        var response = await _loginServices.UpdatePasswordAsync(email, newPass);

        return Ok(response);
    }

    [HttpPatch("email")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> UpdateEmailAsync([FromHeader, Required] string email, string newEmail)
    {
        var response = await _loginServices.UpdateEmailAsync(email, newEmail);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<LoginInfos>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> GetLoginsInfosAsync([FromHeader, Required] string email)
    {
        var response = await _loginServices.GetLoginsInfosAsync(email);

        return Ok(response);
    }
}