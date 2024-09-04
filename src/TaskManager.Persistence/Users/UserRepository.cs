using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Users;
using TaskManager.Persistence.Core;

namespace TaskManager.Persistence.Users;

public sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.Username == username);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Set<User>()
            .FirstOrDefaultAsync(user => user.Email == email);
    }
}