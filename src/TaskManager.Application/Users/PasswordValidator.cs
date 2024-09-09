using System.Text.RegularExpressions;

namespace TaskManager.Application.Users;

public static class PasswordValidator
{
    public static bool IsValid(string password)
    {
        return password.Length is >= 8 and <= 24
               && Regex.IsMatch(password, "[A-Z]")
               && Regex.IsMatch(password, "[a-z]")
               && Regex.IsMatch(password, @"\d")
               && Regex.IsMatch(password, "[@$!%*?&]");
    }
}