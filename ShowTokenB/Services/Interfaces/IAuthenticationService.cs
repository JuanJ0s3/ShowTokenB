namespace ShowTokenB.Services.Interfaces
{ 
    public interface IAuthenticationService
    {
        string GenerateJwtToken(string username);
        Task<string?> Authenticate(string username, string password);
    }
}
