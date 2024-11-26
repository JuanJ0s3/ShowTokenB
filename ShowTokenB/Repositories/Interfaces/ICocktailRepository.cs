using ShowTokenB.Models;

namespace ShowTokenB.Repositories.Interfaces
{
    public interface ICocktailRepository
    {
        Task<List<Drink>> GetDrinks(string url, int limit = 10);
    }
}
