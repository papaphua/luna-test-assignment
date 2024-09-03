namespace TaskManager.Domain.Core;

public abstract class Entity
{
    public Guid Id { get; } = Guid.NewGuid();

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime UpdatedAt { get; set; }
}