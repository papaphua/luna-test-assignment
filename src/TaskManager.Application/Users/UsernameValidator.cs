using System.Text.RegularExpressions;

namespace TaskManager.Application.Users;

public static class UsernameValidator
{
    public static bool IsValid(string username)
    {
        return username.Length is >= 5 and <= 100 && Regex.IsMatch(username, "^[a-zA-Z0-9]+$");
    }
}