using TaskManager.Domain.Core;
using TaskManager.Domain.Users;

namespace TaskManager.Domain.Tasks;

public sealed class Task : Entity
{
    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public Status? Status { get; set; }

    public Priority? Priority { get; set; }

    public Guid OwnerId { get; set; }

    public User Owner { get; set; }
}