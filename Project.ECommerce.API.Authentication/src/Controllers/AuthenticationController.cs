using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Project.ECommerce.API.Authentication.src.Facades.Interfaces;
using Project.ECommerce.API.Authentication.src.Models.Utils;

namespace Project.ECommerce.API.Authentication.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IAuthenticationFacade authenticationFacade) : ControllerBase
    {
        private readonly IAuthenticationFacade _authenticationFacade = authenticationFacade;

        /// <summary>
        /// Generate a token based in email and user identity
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        [HttpGet("login")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTokenAsync([FromHeader, Required] string email, [FromHeader, Required] string password)
        {
            var response = await _authenticationFacade.CreateTokenAsync(email, password);

            return Ok(response);
        }

        /// <summary>
        /// Validate if token is valid or not
        /// </summary>
        /// <param name="token">Token generated</param>
        [HttpGet("validateToken/{token}")]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        public IActionResult ValidateTokenAsync(string token)
        {
            var response = _authenticationFacade.ValidateToken(token);

            return Ok(response);
        }
    }
}