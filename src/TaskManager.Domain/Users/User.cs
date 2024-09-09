using TaskManager.Domain.Core;

namespace TaskManager.Domain.Users;

public sealed class User(string username, string email, string passwordHash) : Entity
{
    public string Username { get; } = username;

    public string Email { get; } = email;

    public string PasswordHash { get; } = passwordHash;
}