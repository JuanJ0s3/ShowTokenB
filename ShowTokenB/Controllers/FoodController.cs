using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShowTokenB.Services.Implementations;
using ShowTokenB.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowTokenB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodService _foodService;
        private IConfiguration _configuration;
        public FoodController(IFoodService foodService, IConfiguration configuration)
        {
            _foodService = foodService;
            _configuration = configuration;
        }
        [HttpGet("foods")]
        public async Task<IActionResult> Get()
        {
            var foodsUrl = _configuration["infoMeal:Url"];
            var result = await _foodService.GetFoods(foodsUrl);
            //if (result == null)
            //{
            //    return NotFound();
            //}
            return Ok(result);
        }

        // Endpoint para buscar comidas por nombre
        [HttpGet("search")]
        public async Task<IActionResult> SearchCoctails([FromQuery] string name, [FromQuery] int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("El parámetro 'name' es obligatorio.");
            }

            // Construir la URL con el nombre de la comida
            var baseUrl = _configuration["infoMeal:Url"];
            var searchUrl = $"{baseUrl}&s={name}";

            var result = await _foodService.GetFoods(searchUrl, limit);

            if (result == null || result.Count == 0)
            {
                return NotFound($"No se encontraron las comidas con el nombre '{name}'.");
            }

            return Ok(result);
        }
    }
}
