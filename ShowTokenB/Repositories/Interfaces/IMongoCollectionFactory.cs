using MongoDB.Driver;

namespace ShowTokenB.Repositories.Interfaces
{
    public interface IMongoCollectionFactory<T>
    {
        IMongoCollection<T> GetCollection(string collectionName);
    }
}
