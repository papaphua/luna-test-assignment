using TaskManager.Domain.Core;
using TaskManager.Domain.Users;

namespace TaskManager.Domain.Tasks;

public sealed class Task : Entity
{
    public string Title { get; }

    public string Description { get; }

    public DateTime DueDate { get; }

    public Status Status { get; }

    public Priority Priority { get; }

    public Guid OwnerId { get; }

    public User Owner { get; }
}