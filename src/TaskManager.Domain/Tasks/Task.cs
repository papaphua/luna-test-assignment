namespace TaskManager.Domain.Tasks;

public sealed class Task
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public DateTime DueDate { get; private set; }

    public Status Status { get; private set; }

    public Priority Priority { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }
}