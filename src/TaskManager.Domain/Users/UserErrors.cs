using TaskManager.Domain.Core.Errors;

namespace TaskManager.Domain.Users;

public static class UserErrors
{
    public static readonly Error EmailExists = Error.Conflict(
        $"{nameof(User)}.{nameof(EmailExists)}", "Email is already registered.");

    public static readonly Error UsernameExists = Error.Conflict(
        $"{nameof(User)}.{nameof(UsernameExists)}", "Username is already registered.");

    public static readonly Error InvalidCredentials = Error.NotFound(
        $"{nameof(User)}.{nameof(InvalidCredentials)}", "Invalid username/email or password.");
}