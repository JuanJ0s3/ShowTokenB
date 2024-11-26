using MongoDB.Driver;
using ShowTokenB.Models;
using ShowTokenB.Repositories.Interfaces;

namespace ShowTokenB.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserRepository(IMongoCollectionFactory<User> collectionFactory)
        {
            _usersCollection = collectionFactory.GetCollection("Users");
        }

        public async Task<User?> GetUserByUsernameAndPassword(string username, string password)
        {
            var filter = Builders<User>.Filter.And(
                Builders<User>.Filter.Eq(u => u.Username, username),
                Builders<User>.Filter.Eq(u => u.Password, password)
            );

            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);
            return await _usersCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task AddUser(User user)
        {
            await _usersCollection.InsertOneAsync(user);
        }
    }
}
