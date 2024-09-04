using TaskManager.Domain.Core;

namespace TaskManager.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);

    Task<User?> GetByEmailAsync(string email);
}