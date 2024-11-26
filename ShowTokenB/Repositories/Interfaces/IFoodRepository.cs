using ShowTokenB.Models;

namespace ShowTokenB.Repositories.Interfaces
{
    public interface IFoodRepository
    {
        //Task<List<Food>> GetMeals(string url, int limit = 10);
         Task<List<Food>> GetFoods(string url, int limit = 10);
    }
}
