using TaskManager.Domain.Core.Errors;

namespace TaskManager.Domain.Tasks;

public static class TaskErrors
{
    public static readonly Error InternalError = Error.Internal(
        $"{nameof(Task)}.{nameof(InternalError)}", "Something went wrong.");

    public static readonly Error TaskNotFound = Error.NotFound(
        $"{nameof(Task)}.{nameof(TaskNotFound)}", "Task not found.");
}