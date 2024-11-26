using ShowTokenB.Models;

namespace ShowTokenB.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameAndPassword(string username, string password);

        Task<User?> GetUserByUsername(string username);
        Task AddUser(User user);
    }
}
