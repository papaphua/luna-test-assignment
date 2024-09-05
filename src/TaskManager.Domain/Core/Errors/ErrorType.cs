namespace TaskManager.Domain.Core.Errors;

public enum ErrorType
{
    _ = 0,
    Validation = 400,
    NotFound = 404,
    Conflict = 409,
    Internal = 500
}