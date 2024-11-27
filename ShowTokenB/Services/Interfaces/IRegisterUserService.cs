namespace ShowTokenB.Services.Interfaces
{
    public interface IRegisterUserService
    {
        Task<bool> RegisterUser(string username, string password);
    }
}
