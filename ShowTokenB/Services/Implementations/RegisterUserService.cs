using ShowTokenB.Models;
using ShowTokenB.Repositories.Interfaces;
using ShowTokenB.Services.Interfaces;

public class RegisterUserService : IRegisterUserService
{
    private readonly IUserRepository _userRepository;

    // Asegúrate de que la inyección de dependencias se haga correctamente
    public RegisterUserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<bool> RegisterUser(string username, string password)
    {
        var existingUser = await _userRepository.GetUserByUsername(username);
        if (existingUser != null) return false;

        var newUser = new User { Username = username, Password = password };
        await _userRepository.AddUser(newUser);
        return true;
    }
}
