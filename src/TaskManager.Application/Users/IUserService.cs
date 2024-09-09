using TaskManager.Application.Users.Dtos;
using TaskManager.Domain.Core.Results;

namespace TaskManager.Application.Users;

public interface IUserService
{
    Task<Result> RegisterAsync(RegisterDto dto);

    Task<Result<string>> LoginAsync(LoginDto dto);
}