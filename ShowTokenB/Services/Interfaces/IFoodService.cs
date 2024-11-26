using ShowTokenB.Models;

namespace ShowTokenB.Services.Interfaces
{
    public interface IFoodService
    {
        Task<List<Food>> GetFoods(string url, int limit = 10);
    }
}
