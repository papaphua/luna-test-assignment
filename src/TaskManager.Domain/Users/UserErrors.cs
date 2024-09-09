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

    public static readonly Error InternalError = Error.Internal(
        $"{nameof(User)}.{nameof(InternalError)}", "Something went wrong.");

    public static readonly Error PasswordValidation = Error.Validation(
        $"{nameof(User)}.{nameof(PasswordValidation)}",
        "Password must be between 8 and 24, contains at least one lower letter, one upper letter, one digit, one special character");

    public static readonly Error UsernameValidation = Error.Validation(
        $"{nameof(User)}.{nameof(UsernameValidation)}",
        "Username must be between 5 and 100, contains latin upper and lower letter and nums.");
}