using MongoDB.Driver;
using Newtonsoft.Json;
using ShowTokenB.Models;
using ShowTokenB.Repositories.Interfaces;
using System.Text.Json.Serialization;

namespace ShowTokenB.Repositories.Implementations
{
    public class FoodRepository : IFoodRepository
    {
        private readonly IMongoCollection<Food> _foods;
        private readonly HttpClient _httpClient;
        public FoodRepository(HttpClient httpClient, IMongoCollectionFactory<Food> factory)
        {
            _httpClient = httpClient;
            _foods = factory.GetCollection("Foods");
        }

        public async Task<List<Food>> GetFoods(string url, int limit = 10)
        {
            var mealsResponse = new List<Food>();
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var JsonString = await response.Content.ReadAsStringAsync();
                    var foodResponse = JsonConvert.DeserializeObject<Meals>(JsonString);

                    if(foodResponse?.meals != null)
                    {
                        mealsResponse = foodResponse.meals.Take(limit).ToList();

                        // Guardar en MongoDB
                        if (mealsResponse.Any())
                        {
                            await _foods.InsertManyAsync(mealsResponse);
                        }
                    }
                }
                else
                {
                    throw new Exception("Error a consumir la api");
                }
                return mealsResponse.Take(limit).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error en el servidor" + ex.Message);
            }
        }
    }
}
