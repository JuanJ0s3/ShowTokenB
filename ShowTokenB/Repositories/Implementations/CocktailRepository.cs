using MongoDB.Driver;
using Newtonsoft.Json;
using ShowTokenB.Models;
using ShowTokenB.Repositories.Interfaces;

namespace ShowTokenB.Repositories.Implementations
{
    public class CocktailRepository : ICocktailRepository
    {
        private readonly IMongoCollection<Drink> _mongoCollectionFactory;
        private readonly HttpClient _httpClient;
        public CocktailRepository(HttpClient httpClient, IMongoCollectionFactory<Drink> factory)
        {
            _httpClient = httpClient;
            _mongoCollectionFactory = factory.GetCollection("Cocktails");
        }

        public async Task<List<Drink>> GetDrinks(string url, int limit = 10)
        {
            var coctailsResponse = new List<Drink>();
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var coctailResponse = JsonConvert.DeserializeObject<Drinks>(jsonString);

                    if(coctailResponse != null)
                    {
                         //coctailsResponse = coctailResponse.drinks.Take(10).ToList();
                        return coctailsResponse = coctailResponse.drinks.Take(10).ToList();
                    }
                    else
                    {
                        throw new Exception("No se pudo deserializar la respuesta de la API de cocteles.");
                    }
                }
                else
                {
                    throw new Exception("Error al consumir la api");
                }
                //return coctailsResponse.Take(limit).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error en el servidor" + ex.Message);
            }
        }
    }
}
