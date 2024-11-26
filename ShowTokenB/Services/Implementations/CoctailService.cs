using ShowTokenB.Models;
using ShowTokenB.Repositories.Interfaces;
using ShowTokenB.Services.Interfaces;

namespace ShowTokenB.Services.Implementations
{
    public class CoctailService : ICoctailService
    {
        private readonly ICocktailRepository _coctailRepository;
        public CoctailService(ICocktailRepository coctailRepository)
        {
            _coctailRepository = coctailRepository;
        }
        public async Task<List<Drink>> GetDrinks(string url, int limit = 10)
        {
            return await _coctailRepository.GetDrinks(url, limit);
        }
    }
}
