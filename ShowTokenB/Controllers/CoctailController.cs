using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowTokenB.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowTokenB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    
    public class CoctailController : ControllerBase
    {
        private readonly ICoctailService _coctailService;
        private IConfiguration _configuration;
        public CoctailController(ICoctailService coctailService, IConfiguration configuration)
        {
            _coctailService = coctailService;
            _configuration = configuration;
        }
        [HttpGet("coctails")]
        public async Task<IActionResult> Get()
        {
            var coctailsUrl = _configuration["infoCoctail:Url"];
            var result = await _coctailService.GetDrinks(coctailsUrl);
            //if (result == null)
            //{
            //    return NotFound();
            //}
            return Ok(result);

        
 
        }

        // Endpoint para buscar cócteles por nombre
        [HttpGet("search")]
        public async Task<IActionResult> SearchCoctails([FromQuery] string name, [FromQuery] int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("El parámetro 'name' es obligatorio.");
            }

            // Construir la URL con el nombre del cóctel
            var baseUrl = _configuration["infoCoctail:Url"];
            var searchUrl = $"{baseUrl}&s={name}";

            var result = await _coctailService.GetDrinks(searchUrl, limit);

            if (result == null || result.Count == 0)
            {
                return NotFound($"No se encontraron cócteles con el nombre '{name}'.");
            }

            return Ok(result);
        }
    }
}
