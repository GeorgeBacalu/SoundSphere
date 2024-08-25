using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using SoundSphere.Core.Mappings;
using SoundSphere.Core.Services.Interfaces;
using SoundSphere.Database.Dtos.Common;
using SoundSphere.Database.Dtos.Request.Auth;
using SoundSphere.Database.Dtos.Request.Pagination;
using SoundSphere.Database.Dtos.Response;
using SoundSphere.Database.Entities;
using SoundSphere.Database.Repositories.Interfaces;
using SoundSphere.Infrastructure.Config;
using SoundSphere.Infrastructure.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static SoundSphere.Database.Constants;

namespace SoundSphere.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) => (_userRepository, _mapper) = (userRepository, mapper);

        public async Task<List<UserDto>> GetAllAsync(UserPaginationRequest payload) => (await _userRepository.GetAllAsync(payload)).ToDtos(_mapper);

        public async Task<UserDto> GetByIdAsync(Guid id) => (await _userRepository.GetByIdAsync(id)).ToDto(_mapper);

        public async Task<UserDto> UpdateByIdAsync(UserDto userDto, Guid id) => (await _userRepository.UpdateByIdAsync(userDto.ToEntity(_mapper), id)).ToDto(_mapper);

        public async Task<UserDto> DeleteByIdAsync(Guid id) => (await _userRepository.DeleteByIdAsync(id)).ToDto(_mapper);

        public async Task<UserDto> RegisterAsync(RegisterRequest payload)
        {
            if (await _userRepository.GetByInfoAsync(payload.Name, payload.Email, payload.Phone) != null) 
                throw new InvalidRequestException(UserAlreadyExists);
            User user = payload.ToEntity();
            user.PasswordSalt = Convert.ToBase64String(GenerateSalt());
            user.PasswordHash = HashPassword(payload.Password, Convert.FromBase64String(user.PasswordSalt));
            _userRepository.AddUserArtist(user);
            _userRepository.AddUserSong(user);
            return (await _userRepository.AddAsync(user)).ToDto(_mapper);
        }

        public async Task<string> LoginAsync(LoginRequest payload)
        {
            User user = await _userRepository.GetByEmailAsync(payload.Email);
            string hashedPassword = HashPassword(payload.Password, Convert.FromBase64String(user.PasswordSalt));
            return hashedPassword == user.PasswordHash ? GenerateToken(user) : throw new InvalidRequestException(InvalidPassword);
        }

        public string GenerateToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            SymmetricSecurityKey secret = new(Encoding.UTF8.GetBytes(AppConfig.JwtSettings.Secret)); // verify encoded signature and set it as secret in appsettings.Development.json
            Claim idClaim = new("userId", user.Id.ToString()); // claims are used to store information about the user and are placed in the second part of the token
            Claim roleClaim = new("role", user.Role.ToString());
            SecurityTokenDescriptor tokenDescriptor = new();
            tokenDescriptor.Issuer = AppConfig.JwtSettings.Issuer;
            tokenDescriptor.Audience = AppConfig.JwtSettings.Audience;
            tokenDescriptor.Subject = new ClaimsIdentity([idClaim, roleClaim]);
            tokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(AppConfig.JwtSettings.TokenValidMinutes);
            tokenDescriptor.SigningCredentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256); // the token will be signed with SHA256 hashing algorithm
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private byte[] GenerateSalt() => RandomNumberGenerator.GetBytes(16);

        private string HashPassword(string password, byte[] salt) => Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1000, numBytesRequested: 32));
    }
}