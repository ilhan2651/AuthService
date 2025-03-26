using Entity.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Abstract;
using Service.Abstract;
using Service.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<AuthResponseDto> LoginUserAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
            await _userRepository.UpdateUserAsync(user);

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                RefreshToken = user.RefreshToken
            };
        }

        public async Task<string> RegisterUserAsync(UserRegisterDto dto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var newUser = new User
            {
                NameSurname = dto.NameSurname,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                DisabilityType = dto.DisabilityType
            };

            await _userRepository.AddUserAsync(newUser);
            return GenerateJwtToken(newUser);
        }

        public async Task<AuthResponseDto> RefreshTokenAsync(string email, string refreshToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired Refresh Token");
            }

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(30);
            await _userRepository.UpdateUserAsync(user);

            return new AuthResponseDto
            {
                Token = GenerateJwtToken(user),
                RefreshToken = user.RefreshToken
            };
        }


        public async Task LogoutUserAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiry = DateTime.UtcNow;
            await _userRepository.UpdateUserAsync(user);
        }


        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim("disability_type", user.DisabilityType.ToString()) // ✅ Enum'u string'e çevir
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        }
        public async Task UpdateUserAsync(UserUpdateDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(dto.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.NameSurname = dto.NameSurname;
            user.Email = dto.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.PasswordHash);

            await _userRepository.UpdateUserAsync(user);
        }


    }
}
