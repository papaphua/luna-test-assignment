using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using TaskManager.Application.Core;
using TaskManager.Application.Users.Dtos;
using TaskManager.Domain.Core.Results;
using TaskManager.Domain.Users;

namespace TaskManager.Application.Users;

public sealed class UserService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IUserService
{
    public async Task<Result> RegisterAsync(RegisterDto dto)
    {
        var isEmailRegistered = await userRepository.GetByEmailAsync(dto.Email) != null;
        var isUsernameRegistered = await userRepository.GetByUsernameAsync(dto.Username) != null;

        if (isEmailRegistered) return Result.Failure(UserErrors.EmailExists);

        if (isUsernameRegistered) return Result.Failure(UserErrors.UsernameExists);

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User(dto.Username, dto.Email, hashedPassword);

        await userRepository.AddAsync(user);

        await unitOfWork.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<string>> LoginAsync(LoginDto dto)
    {
        var user = await userRepository.GetByUsernameAsync(dto.UsernameOrEmail) ??
                   await userRepository.GetByEmailAsync(dto.UsernameOrEmail);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Result<string>.Failure(UserErrors.InvalidCredentials);

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

        return Result<string>.Success(tokenString);
    }
}