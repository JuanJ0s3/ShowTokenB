using ShowTokenB.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ShowTokenB.Repositories.Interfaces;
using ShowTokenB.Repositories.Implementations;
using ShowTokenB.Models;

namespace ShowTokenB.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {

        private IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public async Task<string?> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAndPassword(username, password);
            if (user == null) return null;

            return GenerateJwtToken(user.Username);
        }


        public string GenerateJwtToken(string username)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["JSONWebTocken:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["JSONWebTocken:Issuer"],
                Audience = _configuration["JSONWebTocken:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
