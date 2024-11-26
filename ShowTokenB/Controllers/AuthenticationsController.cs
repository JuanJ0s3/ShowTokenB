using ShowTokenB.Models;
using ShowTokenB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowTokenB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User login)
        {
            var token = await _authenticationService.Authenticate(login.Username, login.Password);
            if (token == null) return Unauthorized();

            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User registration)
        {
            var success = await _authenticationService.RegisterUser(registration.Username, registration.Password);
            if (!success) return BadRequest(new { message = "Username already exists." });

            return Ok(new { message = "User registered successfully." });
        }
    }
}
