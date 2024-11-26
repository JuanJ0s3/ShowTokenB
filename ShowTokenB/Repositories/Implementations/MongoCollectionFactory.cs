using MongoDB.Driver;
using ShowTokenB.Repositories.Interfaces;

namespace ShowTokenB.Repositories.Implementations
{
    public class MongoCollectionFactory<T> : IMongoCollectionFactory<T>
    {
        private readonly IMongoDatabase _database;

        public MongoCollectionFactory(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("MongoDBSettings:MongoDb").Value;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("MongoDBSettings:MongoDb", "La cadena de conexión de MongoDb no está configurada correctamente.");
            }

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("ComidasBebidasdb");
        }

        public IMongoCollection<T> GetCollection(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
