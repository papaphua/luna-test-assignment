using TaskManager.Domain.Core;

namespace TaskManager.Domain.Tasks;

public sealed class Task : Entity
{
    public string Title { get; }

    public string Description { get; }

    public DateTime DueDate { get; }

    public Status Status { get; }

    public Priority Priority { get; }
}