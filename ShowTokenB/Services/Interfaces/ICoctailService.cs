using ShowTokenB.Models;

namespace ShowTokenB.Services.Interfaces
{
    public interface ICoctailService
    {
        Task<List<Drink>> GetDrinks(string url, int limit = 10);

    }
}
