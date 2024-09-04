using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.Core;
using TaskManager.Application.Users.Dtos;
using TaskManager.Domain.Users;

namespace TaskManager.Application.Users;

public sealed class UserService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IUserService
{
    public async Task RegisterAsync(RegisterDto dto)
    {
        var isUsernameRegistered = await userRepository.GetByUsernameAsync(dto.Username) != null;
        var isEmailRegistered = await userRepository.GetByEmailAsync(dto.Email) != null;

        if (isUsernameRegistered) return;

        if (isEmailRegistered) return;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User(dto.Username, dto.Email, hashedPassword);

        await userRepository.AddAsync(user);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var user = await userRepository.GetByUsernameAsync(dto.UsernameOrEmail) ??
                   await userRepository.GetByEmailAsync(dto.UsernameOrEmail);

        if (user == null || BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) return string.Empty;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(Env.GetString("JWT_KEY"));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = Env.GetString("JWT_ISSUER"),
            Audience = Env.GetString("JWT_AUDIENCE"),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}