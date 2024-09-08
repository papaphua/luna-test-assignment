namespace TaskManager.Domain.Tasks;

public sealed class TaskFilter
{
    public Status? Status { get; set; } = null;

    public DateTime? DueDate { get; set; } = null;

    public Priority? Priority { get; set; } = null;
}