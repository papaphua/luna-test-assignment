using TaskManager.Domain.Core;

namespace TaskManager.Domain.Users;

public sealed class User : Entity
{
    public string Username { get; }

    public string Email { get; }

    public string PasswordHash { get; }
}