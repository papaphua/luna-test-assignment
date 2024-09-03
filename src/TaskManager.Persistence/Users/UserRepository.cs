using TaskManager.Domain.Users;
using TaskManager.Persistence.Core;

namespace TaskManager.Persistence.Users;

public sealed class UserRepository(ApplicationDbContext dbContext) : Repository<User>(dbContext), IUserRepository;