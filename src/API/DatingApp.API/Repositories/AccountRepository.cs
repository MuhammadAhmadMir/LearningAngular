using DatingApp.API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApp.API.Repositories
{
    [ScopedService]
    public class AccountRepository(UserManager<IdentityUser> _userManager, IConfiguration _configuration) : IAccountRepository
    {
        public async Task<bool> CreateUserAccountAsync(RegisterAccountDto dto)
        {
            var user = new IdentityUser
            {
                UserName = dto.Username,
                Email = dto.Username
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            return result.Succeeded;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Username);
            if(user == null)
            {
                return null;
            }

            var validatePassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if(!validatePassword)
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseDto(user.UserName, user.Email, token);
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["Secret"]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
