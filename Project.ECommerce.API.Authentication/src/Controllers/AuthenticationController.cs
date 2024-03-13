using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project.ECommerce.API.Authentication.src.Facades.Interfaces;

namespace Project.ECommerce.API.Authentication.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationFacade authenticationFacade) : ControllerBase
    {
        private readonly IAuthenticationFacade _authenticationFacade = authenticationFacade;

        [HttpGet("login")]
        public async Task<IActionResult> CreateTokenAsync([FromHeader, Required] string email, [FromHeader, Required] string password)
        {
            var response = await _authenticationFacade.CreateTokenAsync(email, password);

            return Ok(response);
        }

        [HttpGet("validateToken/{token}")]
        public IActionResult ValidateTokenAsync(string token)
        {
            var response = _authenticationFacade.ValidateToken(token);

            return Ok(response);
        }
    }
}