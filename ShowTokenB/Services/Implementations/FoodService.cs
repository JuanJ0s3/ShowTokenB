using ShowTokenB.Models;
using ShowTokenB.Repositories.Implementations;
using ShowTokenB.Repositories.Interfaces;
using ShowTokenB.Services.Interfaces;

namespace ShowTokenB.Services.Implementations
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _foodRepository;
        public FoodService(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        public async Task<List<Food>> GetFoods(string url, int limit = 10)
        {
            
            return await _foodRepository.GetFoods(url, limit);
        }
    }
}
