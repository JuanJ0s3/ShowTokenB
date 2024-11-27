using Microsoft.AspNetCore.Mvc;
using ShowTokenB.Models;
using ShowTokenB.Services.Interfaces;

public class RegisterUsersController : ControllerBase
{
    private readonly IRegisterUserService _registerService;

    public RegisterUsersController(IRegisterUserService registerService)
    {
        _registerService = registerService ?? throw new ArgumentNullException(nameof(registerService));
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User registration)
    {
        if (registration == null)
        {
            return BadRequest("Datos del usuario no válidos.");
        }

        var success = await _registerService.RegisterUser(registration.Username, registration.Password);
        if (!success) return BadRequest(new { message = "El usuario ya existe" });

        return Ok(new { message = "Usuario registrado correctamente." });
    }
}
